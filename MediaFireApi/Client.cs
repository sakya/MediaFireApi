using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using MediaFireApi.Exceptions;
using MediaFireApi.Models;
using MediaFireApi.Models.Request;
using MediaFireApi.Models.Response;
using Newtonsoft.Json;
using Timer = System.Timers.Timer;

namespace MediaFireApi
{
    /// <summary>
    /// The MediaFire client
    /// </summary>
    public partial class Client : IDisposable
    {
        public const string RootFolderKey = "myfiles";
        public const string TrashFolderKey = "trash";

        internal class ApiCallResponse
        {
            public ApiCallResponse(HttpStatusCode statusCode, string content)
            {
                StatusCode = statusCode;
                Content = content;
            }

            public HttpStatusCode StatusCode { get; set; }
            public string Content { get; set; }

            public bool IsSuccessStatusCode => (int)StatusCode >= 200 && (int)StatusCode <= 299;
        }

        // https://www.mediafire.com/developers/core_api/1.5/getting_started/
        private const string ApiBaseAddress = "https://www.mediafire.com/api/1.5/";
        private string _sessionToken;
        private readonly Timer _sessionTimer;
        private SemaphoreSlim _sessionSema = new SemaphoreSlim(1, 1);
        private DateTime? _lastSessionRenew;
        private readonly HttpClient _client;
        private readonly HttpClientHandler _clientHandler;

        public Client(ClientSettings settings)
        {
            Settings = settings;
            ValidateSettings();

            _clientHandler = new HttpClientHandler() { CookieContainer = new CookieContainer() };
            _client = new HttpClient(_clientHandler);
            _sessionTimer = new Timer(Settings.SessionKeepAliveTimeoutMs);
            _sessionTimer.Elapsed += OnSessionTimer;
            _sessionTimer.Start();
        }

        public ClientSettings Settings { get; private set; }

        public void Dispose()
        {
            _sessionTimer?.Stop();
            _sessionTimer?.Dispose();
            _client?.Dispose();
            _clientHandler?.Dispose();
        }

        #region private operations
        private void ValidateSettings()
        {
            if (Settings.SessionKeepAliveTimeoutMs <= 0)
                throw new Exception($"{nameof(Settings.SessionKeepAliveTimeoutMs)} must be greater than zero");
        }

        private async void OnSessionTimer(object source, ElapsedEventArgs e)
        {
            if (!string.IsNullOrEmpty(_sessionToken) && _lastSessionRenew <= DateTime.UtcNow.AddMinutes(-9)) {
                await UserRenewSessionToken();
            }
        }


        private Uri GetApiUri(string action)
        {
            return new Uri($"{ApiBaseAddress}{action}");
        }

        private async Task<ApiCallResponse> GetApiResponse(Uri uri, HttpContent content)
        {
            using (var res = await _client.PostAsync(uri, content)) {
                var result = new ApiCallResponse(res.StatusCode, await res.Content.ReadAsStringAsync());
                if (!result.IsSuccessStatusCode) {
                    var apiResponse = JsonConvert.DeserializeObject<ResponseModel<ApiErrorResponse>>(result.Content);
                    if (apiResponse?.Response.Result == ApiResult.Error)
                        throw new MediaFireApiException(apiResponse.Response.Error, apiResponse.Response.Message);
                }
                return result;
            }
        }

        private async Task CheckSessionToken()
        {
            if (string.IsNullOrEmpty(_sessionToken))
                throw new Exception("Not logged in");
        }

        private void CheckApiResponse<T>(ResponseModel<T> apiResponse, string errorMessage) where T : ApiResponse
        {
            if (apiResponse == null || apiResponse.Response == null)
                throw new Exception(errorMessage);
            if (apiResponse.Response.Result == ApiResult.Error)
                throw new MediaFireApiException(apiResponse.Response.Error, apiResponse.Response.Message);
        }

        private FormUrlEncodedContent ToFormUrlEncodedContent(ApiRequest model)
        {
            var values = new Dictionary<string, string>();
            foreach (var p in model.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public)) {
                var name = p.Name;
                var attr = p.GetCustomAttribute<JsonPropertyAttribute>(true);
                if (attr != null && !string.IsNullOrEmpty(attr.PropertyName))
                    name = attr.PropertyName;

                var value = p.GetValue(model);
                if (p.PropertyType.IsEnum && value != null)
                    value = value.ToString().ToLower();

                values[name] = value?.ToString();
            }
            return new FormUrlEncodedContent(values);
        }
        #endregion
    }
}