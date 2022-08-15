using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using MediaFireApi.Models;
using MediaFireApi.Models.Request;
using MediaFireApi.Models.Response;
using Newtonsoft.Json;

namespace MediaFireApi
{
    public partial class Client : IDisposable
    {
        // https://www.mediafire.com/developers/core_api/1.5/getting_started/
        private const string ApiBaseAddress = "https://www.mediafire.com/api/1.5/";
        private string _sessionToken = null;
        private DateTime? _lastSessionRenew;
        private HttpClient _client;
        private HttpClientHandler _clientHandler;

        public Client(ClientSettings settings)
        {
            Settings = settings;

            _clientHandler = new HttpClientHandler() { CookieContainer = new CookieContainer() };
            _client = new HttpClient(_clientHandler);
        }

        public ClientSettings Settings { get; private set; }

        public void Dispose()
        {
            _client?.Dispose();
            _clientHandler?.Dispose();
        }

        #region private operations

        private Uri GetApiUri(string action)
        {
            return new Uri($"{ApiBaseAddress}{action}");
        }

        private async Task CheckSessionToken()
        {
            if (string.IsNullOrEmpty(_sessionToken))
                throw new Exception("Not logged in");

            if (_lastSessionRenew <= DateTime.UtcNow.AddMinutes(-10)) {
                await RenewSessionToken();
            }
        }

        private void CheckApiResponse<T>(ResponseModel<T> apiResponse, string errorMessage) where T : ApiResponse
        {
            if (apiResponse == null || apiResponse.Response == null)
                throw new Exception(errorMessage);
            if (apiResponse.Response.Result == ApiResult.Error)
                throw new Exception($"{errorMessage}: {apiResponse.Response.Message}");
        }

        private FormUrlEncodedContent ToFormUrlEncodedContent(RequestModel model)
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