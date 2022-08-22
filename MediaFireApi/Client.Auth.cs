using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using MediaFireApi.Models.Request;
using MediaFireApi.Models.Response;
using Newtonsoft.Json;

namespace MediaFireApi
{
    public partial class Client
    {
        private const string SecurityElement = "<input type=\"hidden\" name=\"security\" value=\"";

        /// <summary>
        /// Login to the user account
        /// </summary>
        /// <param name="userEmail">The user email</param>
        /// <param name="password">The user password</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="Exception"></exception>
        public async Task Login(string userEmail, string password)
        {
            if (string.IsNullOrEmpty(userEmail))
                throw new ArgumentNullException(nameof(userEmail));
            if (string.IsNullOrEmpty(password))
                throw new ArgumentNullException(nameof(password));

            string security = null;
            using (var loginRes = await _client.GetAsync("https://www.mediafire.com/login/")) {
                var resContent = await loginRes.Content.ReadAsStringAsync();
                if (!string.IsNullOrEmpty(resContent)) {
                    var idx = resContent.IndexOf(SecurityElement, StringComparison.InvariantCultureIgnoreCase);
                    if (idx >= 0) {
                        var endIdx = resContent.IndexOf("\"", idx + SecurityElement.Length,
                            StringComparison.InvariantCultureIgnoreCase);
                        security = resContent.Substring(idx + SecurityElement.Length,
                            endIdx - SecurityElement.Length - idx);
                    }
                }
            }

            if (string.IsNullOrEmpty(security))
                throw new Exception("Cannot get security code");

            var content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "security", security},
                { "login_email", userEmail},
                { "login_pass", password},
                { "login_remember", "false"}
            });
            _client.DefaultRequestHeaders.Add("Referer", "https://www.mediafire.com/login/");
            using (var loginRes = await _client.PostAsync(
                       new Uri("https://www.mediafire.com/dynamic/client_login/mediafire.php"), content)) {
                var resContent = await loginRes.Content.ReadAsStringAsync();
                if (loginRes.IsSuccessStatusCode) {
                    using (var tokenRes = await _client.PostAsync(
                               new Uri("https://www.mediafire.com/application/get_session_token.php"), null)) {
                        resContent = await tokenRes.Content.ReadAsStringAsync();
                        if (tokenRes.IsSuccessStatusCode) {
                            var tokenResObj = JsonConvert.DeserializeObject<ResponseModel<SessionTokenModel>>(resContent);
                            if (tokenResObj?.Response == null || string.IsNullOrEmpty(tokenResObj.Response.SessionToken))
                                throw new Exception("Cannot get session token");
                            _sessionToken = tokenResObj.Response.SessionToken;
                            _lastSessionRenew = DateTime.UtcNow;
                        } else {
                            throw new Exception(resContent);
                        }
                    }
                } else {
                    throw new Exception(resContent);
                }
            }
        }

        /// <summary>
        /// Logout the current user
        /// </summary>
        /// <exception cref="Exception"></exception>
        public async Task Logout()
        {
            await CheckSessionToken();

            await _sessionSema.WaitAsync();
            var req = new ApiRequest()
            {
                SessionToken = _sessionToken
            };

            using (var res = await _client.PostAsync(new Uri("https://www.mediafire.com/application/logout.php"),
                       ToFormUrlEncodedContent(req))) {
                var resContent = await res.Content.ReadAsStringAsync();
                if (!res.IsSuccessStatusCode)
                    throw new Exception(resContent);
            }

            _sessionToken = null;
            _lastSessionRenew = null;
            _sessionSema.Release();
        }
    }
}