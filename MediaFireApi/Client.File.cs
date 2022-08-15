using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediaFireApi.Models.Request;
using MediaFireApi.Models.Response;
using Newtonsoft.Json;

namespace MediaFireApi
{
    public partial class Client
    {
        public async Task<FileInfoResponse.FileInfoModel> FileGetInfo(string folderKey)
        {
            var res = await FileGetInfo(new[] { folderKey });
            return res.Count > 0 ? res[0] : null;
        }

        /// <summary>
        /// Get files info
        /// </summary>
        /// <param name="quickKeys">File keys</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="Exception"></exception>
        public async Task<List<FileInfoResponse.FileInfoModel>> FileGetInfo(IEnumerable<string> quickKeys)
        {
            if (quickKeys == null)
                throw new ArgumentNullException(nameof(quickKeys));
            await CheckSessionToken();

            var req = new FileInfoRequest()
            {
                SessionToken = _sessionToken,
                QuickKey = string.Join(",", quickKeys)
            };
            var res = await _client.PostAsync(GetApiUri("file/get_info.php"), ToFormUrlEncodedContent(req));
            var resContent = await res.Content.ReadAsStringAsync();
            if (!res.IsSuccessStatusCode)
                throw new Exception(resContent);

            var jsonRes = JsonConvert.DeserializeObject<ResponseModel<FileInfoResponse>>(resContent);
            CheckApiResponse(jsonRes, "Cannot get file info");

            if (jsonRes?.Response.FileInfos == null && jsonRes?.Response.FileInfo != null)
                return new List<FileInfoResponse.FileInfoModel>() { jsonRes?.Response.FileInfo };
            return jsonRes?.Response.FileInfos;
        }
    }
}