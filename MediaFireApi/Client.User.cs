using System;
using System.Threading.Tasks;
using MediaFireApi.Models.Request;
using MediaFireApi.Models.Response;
using Newtonsoft.Json;

namespace MediaFireApi
{
    public partial class Client
    {
        public async Task<UserInfoResponse.UserInfoModel> UserGetInfo()
        {
            await CheckSessionToken();

            var req = new RequestModel()
            {
                SessionToken = _sessionToken
            };
            var res = await GetApiResponse(GetApiUri("user/get_info.php"), ToFormUrlEncodedContent(req));
            if (!res.IsSuccessStatusCode)
                throw new Exception(res.Content);

            var jsonRes = JsonConvert.DeserializeObject<ResponseModel<UserInfoResponse>>(res.Content);
            if (jsonRes?.Response?.UserInfo == null)
                throw new Exception("Cannot get user info");
            return jsonRes.Response.UserInfo;
        }

        public async Task UserRenewSessionToken()
        {
            await CheckSessionToken();

            var req = new RequestModel()
            {
                SessionToken = _sessionToken
            };
            var res = await GetApiResponse(GetApiUri("user/renew_session_token.php"), ToFormUrlEncodedContent(req));
            if (!res.IsSuccessStatusCode)
                throw new Exception(res.Content);

            var jsonRes = JsonConvert.DeserializeObject<ResponseModel<RenewSessionTokenResponse>>(res.Content);
            if (jsonRes?.Response == null || string.IsNullOrEmpty(jsonRes.Response.SessionToken))
                throw new Exception("Cannot get session token");
            _sessionToken = jsonRes.Response.SessionToken;
            _lastSessionRenew = DateTime.UtcNow;
        }
    }
}