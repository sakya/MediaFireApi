using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediaFireApi.Models;
using MediaFireApi.Models.Request;
using MediaFireApi.Models.Response;
using Newtonsoft.Json;

namespace MediaFireApi
{
    public partial class Client
    {
        public async Task<FolderInfoResponse.FolderInfoModel> FolderGetInfo(string folderKey)
        {
            if (string.IsNullOrEmpty(folderKey))
                throw new ArgumentNullException(nameof(folderKey));
            await CheckSessionToken();

            var req = new FolderInfoRequest()
            {
                SessionToken = _sessionToken,
                FolderKey = folderKey
            };
            var res = await _client.PostAsync(GetApiUri("folder/get_info.php"), ToFormUrlEncodedContent(req));
            var resContent = await res.Content.ReadAsStringAsync();
            if (!res.IsSuccessStatusCode)
                throw new Exception(resContent);

            var jsonRes = JsonConvert.DeserializeObject<ResponseModel<FolderInfoResponse>>(resContent);
            if (jsonRes?.Response?.FolderInfo == null)
                throw new Exception("Cannot get folder/get_info");

            return jsonRes.Response.FolderInfo;
        }

        public async Task<FolderContentResponse.FolderContentModel> FolderGetContent(string folderKey, FolderContentType contentType, int chunk = 1, int chunksize = 100)
        {
            if (string.IsNullOrEmpty(folderKey))
                throw new ArgumentNullException(nameof(folderKey));
            await CheckSessionToken();

            var req = new FolderContentRequest()
            {
                SessionToken = _sessionToken,
                FolderKey = folderKey,

                ContentType = contentType,
                ChunkSize = chunksize,
                Chunk = chunk
            };
            var res = await _client.PostAsync(GetApiUri("folder/get_content.php"), ToFormUrlEncodedContent(req));
            var resContent = await res.Content.ReadAsStringAsync();
            if (!res.IsSuccessStatusCode)
                throw new Exception(resContent);

            var jsonRes = JsonConvert.DeserializeObject<ResponseModel<FolderContentResponse>>(resContent);
            if (jsonRes?.Response?.FolderContent?.Files == null && jsonRes?.Response?.FolderContent?.Folders == null)
                throw new Exception("Cannot get folder/get_content");

            return jsonRes.Response.FolderContent;
        }

        public async Task<string> FolderCreate(string parentKey, string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));
            await CheckSessionToken();

            var req = new FolderCreateRequest()
            {
                SessionToken = _sessionToken,
                ParentKey = parentKey,
                FolderName = name
            };
            var res = await _client.PostAsync(GetApiUri("folder/create.php"), ToFormUrlEncodedContent(req));
            var resContent = await res.Content.ReadAsStringAsync();
            if (!res.IsSuccessStatusCode)
                throw new Exception(resContent);

            var jsonRes = JsonConvert.DeserializeObject<ResponseModel<FolderCreateResponse>>(resContent);
            if (jsonRes?.Response.FolderKey == null)
                throw new Exception("Cannot create folder");

            return jsonRes.Response.FolderKey;
        }

        public async Task<bool> FolderDelete(string folderKey)
        {
            if (string.IsNullOrEmpty(folderKey))
                throw new ArgumentNullException(nameof(folderKey));
            await CheckSessionToken();

            var req = new FolderDeleteRequest()
            {
                SessionToken = _sessionToken,
                FolderKey = folderKey
            };
            var res = await _client.PostAsync(GetApiUri("folder/delete.php"), ToFormUrlEncodedContent(req));
            var resContent = await res.Content.ReadAsStringAsync();
            if (!res.IsSuccessStatusCode)
                throw new Exception(resContent);

            var jsonRes = JsonConvert.DeserializeObject<ResponseModel<FolderDeleteResponse>>(resContent);
            if (jsonRes == null)
                throw new Exception("Cannot delete folder");

            return true;
        }
    }
}