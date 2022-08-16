using System;
using System.IO;
using System.Threading.Tasks;
using MediaFireApi.Models;
using MediaFireApi.Models.Request;
using MediaFireApi.Models.Response;
using Newtonsoft.Json;

namespace MediaFireApi
{
    public partial class Client
    {
        public async Task<UploadCheckResponse> UploadCheck(string folderKey, string fileName, long size, bool resumable = false, string hash = null)
        {
            await CheckSessionToken();

            var req = new UploadCheckRequest()
            {
                SessionToken = _sessionToken,

                FileName = fileName,
                FolderKey = folderKey,
                Size = size,
                Resumable = resumable ? YesNo.Yes : YesNo.No,
                Hash = hash
            };
            var res = await GetApiResponse(GetApiUri("upload/check.php"), ToFormUrlEncodedContent(req));
            if (!res.IsSuccessStatusCode)
                throw new Exception(res.Content);

            var jsonRes = JsonConvert.DeserializeObject<ResponseModel<UploadCheckResponse>>(res.Content);
            CheckApiResponse(jsonRes, "Cannot check upload");

            return jsonRes.Response;
        }

        /*public async Task<UploadCheckResponse> UploadSimple(Stream stream, string contentType)
        {
            await CheckSessionToken();
        }*/
    }
}