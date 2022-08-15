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

        public async Task Login(string userEmail, string password)
        {
            if (string.IsNullOrEmpty(userEmail))
                throw new ArgumentNullException(nameof(userEmail));
            if (string.IsNullOrEmpty(password))
                throw new ArgumentNullException(nameof(password));

            string security = null;
            var res = await _client.GetAsync("https://www.mediafire.com/login/");
            var resContent = await res.Content.ReadAsStringAsync();
            if (!string.IsNullOrEmpty(resContent)) {
                var idx = resContent.IndexOf(SecurityElement, StringComparison.InvariantCultureIgnoreCase);
                if (idx >= 0) {
                    var endIdx = resContent.IndexOf("\"", idx + SecurityElement.Length, StringComparison.InvariantCultureIgnoreCase);
                    security = resContent.Substring(idx + SecurityElement.Length, endIdx - SecurityElement.Length - idx);
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
            res = await _client.PostAsync(new Uri("https://www.mediafire.com/dynamic/client_login/mediafire.php"), content);
            resContent = await res.Content.ReadAsStringAsync();
            if (res.IsSuccessStatusCode) {
                res = await _client.PostAsync(new Uri("https://www.mediafire.com/application/get_session_token.php"), null);
                resContent = await res.Content.ReadAsStringAsync();
                if (res.IsSuccessStatusCode) {
                    var tokenRes = JsonConvert.DeserializeObject<ResponseModel<SessionTokenModel>>(resContent);
                    if (tokenRes?.Response == null || string.IsNullOrEmpty(tokenRes.Response.SessionToken))
                        throw new Exception("Cannot get session token");
                    _sessionToken = tokenRes.Response.SessionToken;
                    _lastSessionRenew = DateTime.UtcNow;
                } else {
                    throw new Exception(resContent);
                }
            } else {
                throw new Exception(resContent);
            }
        }

        public async Task RenewSessionToken()
        {
            if (string.IsNullOrEmpty(_sessionToken))
                throw new Exception("Not logged in");

            var req = new RequestModel()
            {
                SessionToken = _sessionToken
            };
            var res = await _client.PostAsync(GetApiUri("user/renew_session_token.php"), ToFormUrlEncodedContent(req));
            var resContent = await res.Content.ReadAsStringAsync();
            if (!res.IsSuccessStatusCode)
                throw new Exception(resContent);

            var tokenRes = JsonConvert.DeserializeObject<ResponseModel<RenewSessionTokenResponse>>(resContent);
            if (tokenRes?.Response == null || string.IsNullOrEmpty(tokenRes.Response.SessionToken))
                throw new Exception("Cannot get session token");
            _sessionToken = tokenRes.Response.SessionToken;
            _lastSessionRenew = DateTime.UtcNow;
        }

        public async Task Logout()
        {
            if (string.IsNullOrEmpty(_sessionToken))
                throw new Exception("Not logged in");

            var req = new RequestModel()
            {
                SessionToken = _sessionToken
            };
            var res = await _client.PostAsync(new Uri("https://www.mediafire.com/application/logout.php"), ToFormUrlEncodedContent(req));
            var resContent = await res.Content.ReadAsStringAsync();
            if (!res.IsSuccessStatusCode)
                throw new Exception(resContent);
            _sessionToken = null;
        }
    }
}