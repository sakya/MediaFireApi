using System;
using System.Threading.Tasks;
using MediaFireApi.Models.Request;
using MediaFireApi.Models.Response;
using Newtonsoft.Json;

namespace MediaFireApi
{
    public partial class Client
    {
        /// <summary>
        /// Returns a list of the user's personal information and account vitals.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<UserInfoResponse.UserInfoModel> UserGetInfo()
        {
            await CheckSessionToken();

            var req = new ApiRequest()
            {
                SessionToken = _sessionToken
            };
            var res = await GetApiResponse(GetApiUri("user/get_info.php"), ToFormUrlEncodedContent(req));
            var jsonRes = JsonConvert.DeserializeObject<ResponseModel<UserInfoResponse>>(res.Content);
            if (jsonRes?.Response?.UserInfo == null)
                throw new Exception("Cannot get user info");
            return jsonRes.Response.UserInfo;
        }

        /// <summary>
        /// Extends the life of the session token by another 10 minutes.
        /// If the session token is less than 5 minutes old, then it does not get renewed and the same token is returned.
        /// If the token is more than 5 minutes old, then, depending on the application configuration, the token gets extended or a new token is generated and returned.
        /// </summary>
        /// <exception cref="Exception"></exception>
        public async Task UserRenewSessionToken()
        {
            if (string.IsNullOrEmpty(_sessionToken))
                throw new Exception("Not logged in");

            await _sessionSema.WaitAsync();
            try {
                var req = new ApiRequest()
                {
                    SessionToken = _sessionToken
                };
                var res = await GetApiResponse(GetApiUri("user/renew_session_token.php"), ToFormUrlEncodedContent(req));
                var jsonRes = JsonConvert.DeserializeObject<ResponseModel<RenewSessionTokenResponse>>(res.Content);
                if (jsonRes?.Response == null || string.IsNullOrEmpty(jsonRes.Response.SessionToken))
                    throw new Exception("Cannot get session token");
                _sessionToken = jsonRes.Response.SessionToken;
                _lastSessionRenew = DateTime.UtcNow;
            } finally {
                _sessionSema.Release();
            }
        }
    }
}