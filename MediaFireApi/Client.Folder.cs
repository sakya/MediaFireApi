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
        public async Task<List<FolderItem>> FolderGetInfo(IEnumerable<string> folderKeys = null, string folderPath = null)
        {
            if (folderKeys == null && string.IsNullOrEmpty(folderPath))
                throw new ArgumentException("FolderKeys or folderPath must be provided");
            await CheckSessionToken();

            var req = new FolderInfoRequest()
            {
                SessionToken = _sessionToken,
                FolderKey = folderKeys != null ? string.Join(",", folderKeys) : null,
                FolderPath = folderPath
            };
            var res = await GetApiResponse(GetApiUri("folder/get_info.php"), ToFormUrlEncodedContent(req));
            if (!res.IsSuccessStatusCode)
                throw new Exception(res.Content);

            var jsonRes = JsonConvert.DeserializeObject<ResponseModel<FolderInfoResponse>>(res.Content);
            CheckApiResponse(jsonRes, "Cannot get folder info");

            if (jsonRes?.Response.FolderInfos == null && jsonRes?.Response.FolderItemInfo != null)
                return new List<FolderItem>() { jsonRes.Response.FolderItemInfo };
            return jsonRes?.Response.FolderInfos;
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
            var res = await GetApiResponse(GetApiUri("folder/get_content.php"), ToFormUrlEncodedContent(req));
            if (!res.IsSuccessStatusCode)
                throw new Exception(res.Content);

            var jsonRes = JsonConvert.DeserializeObject<ResponseModel<FolderContentResponse>>(res.Content);
            CheckApiResponse(jsonRes, "Cannot get folder content");

            return jsonRes?.Response.FolderContent;
        }

        /// <summary>
        /// Create a folder
        /// </summary>
        /// <param name="parentKey">The parent folder key</param>
        /// <param name="name">The new folder name</param>
        /// <param name="actionOnDuplicate">The <see cref="ActionOnDuplicate"/></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="Exception"></exception>
        public async Task<string> FolderCreate(string parentKey, string name, ActionOnDuplicate actionOnDuplicate = ActionOnDuplicate.Keep)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));
            await CheckSessionToken();

            var req = new FolderCreateRequest()
            {
                SessionToken = _sessionToken,
                ParentKey = parentKey,
                FolderName = name,
                ActionOnDuplicate = actionOnDuplicate
            };
            var res = await GetApiResponse(GetApiUri("folder/create.php"), ToFormUrlEncodedContent(req));
            if (!res.IsSuccessStatusCode)
                throw new Exception(res.Content);

            var jsonRes = JsonConvert.DeserializeObject<ResponseModel<FolderCreateResponse>>(res.Content);
            CheckApiResponse(jsonRes, "Cannot create folder");

            return jsonRes?.Response.FolderKey;
        }

        /// <summary>
        /// Move folders to the trash can
        /// </summary>
        /// <param name="folderKeys">Folder keys</param>
        /// <param name="folderPath">Folder path</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="Exception"></exception>
        public async Task<bool> FolderDelete(IEnumerable<string> folderKeys = null, string folderPath = null)
        {
            if (folderKeys == null && string.IsNullOrEmpty(folderPath))
                throw new ArgumentException("FolderKeys or folderPath must be provided");
            await CheckSessionToken();

            var req = new FolderDeleteRequest()
            {
                SessionToken = _sessionToken,
                FolderKey = folderKeys != null ? string.Join(",", folderKeys) : null,
                FolderPath = folderPath
            };
            var res = await GetApiResponse(GetApiUri("folder/delete.php"), ToFormUrlEncodedContent(req));
            if (!res.IsSuccessStatusCode)
                throw new Exception(res.Content);

            var jsonRes = JsonConvert.DeserializeObject<ResponseModel<FolderDeleteResponse>>(res.Content);
            CheckApiResponse(jsonRes, "Cannot delete folder");

            return true;
        }

        /// <summary>
        /// Delete folders permanently
        /// </summary>
        /// <param name="folderKeys">Folder keys</param>
        /// <param name="folderPath">Folder path</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="Exception"></exception>
        public async Task<bool> FolderPurge(IEnumerable<string> folderKeys = null, string folderPath = null)
        {
            if (folderKeys == null && string.IsNullOrEmpty(folderPath))
                throw new ArgumentException("FolderKeys or folderPath must be provided");
            await CheckSessionToken();

            var req = new FolderDeleteRequest()
            {
                SessionToken = _sessionToken,
                FolderKey = folderKeys != null ? string.Join(",", folderKeys) : null,
                FolderPath = folderPath
            };
            var res = await GetApiResponse(GetApiUri("folder/purge.php"), ToFormUrlEncodedContent(req));
            if (!res.IsSuccessStatusCode)
                throw new Exception(res.Content);

            var jsonRes = JsonConvert.DeserializeObject<ResponseModel<FolderDeleteResponse>>(res.Content);
            CheckApiResponse(jsonRes, "Cannot purge folder");

            return true;
        }

        /// <summary>
        /// Copy folders to a target folder
        /// </summary>
        /// <param name="folderKeys">Folder keys</param>
        /// <param name="folderPath">Folder path</param>
        /// <param name="targetFolderKey">The target folder key</param>
        /// <returns>The keys of the newly created folders</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="Exception"></exception>
        public async Task<string[]> FolderCopy(IEnumerable<string> folderKeys = null, string folderPath = null, string targetFolderKey = null)
        {
            if (folderKeys == null && string.IsNullOrEmpty(folderPath))
                throw new ArgumentException("FolderKeys or folderPath must be provided");
            if (string.IsNullOrEmpty(targetFolderKey))
                throw new ArgumentNullException(nameof(targetFolderKey));
            await CheckSessionToken();

            var req = new FolderCopyRequest()
            {
                SessionToken = _sessionToken,
                FolderKeySrc = folderKeys != null ? string.Join(",", folderKeys) : null,
                FolderKeyDst = targetFolderKey,
                FolderPath = folderPath
            };
            var res = await GetApiResponse(GetApiUri("folder/copy.php"), ToFormUrlEncodedContent(req));
            if (!res.IsSuccessStatusCode)
                throw new Exception(res.Content);

            var jsonRes = JsonConvert.DeserializeObject<ResponseModel<FolderCopyResponse>>(res.Content);
            CheckApiResponse(jsonRes, "Cannot copy folder");

            return jsonRes?.Response?.NewFolderKey.Split(',');
        }

        /// <summary>
        /// Move folders to a target folder
        /// </summary>
        /// <param name="folderKeys">Folder keys</param>
        /// <param name="folderPath">Folder path</param>
        /// <param name="targetFolderKey">The target folder key</param>
        /// <returns>True on success</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="Exception"></exception>
        public async Task<bool> FolderMove(IEnumerable<string> folderKeys = null, string folderPath = null, string targetFolderKey = null)
        {
            if (folderKeys == null && string.IsNullOrEmpty(folderPath))
                throw new ArgumentException("FolderKeys or folderPath must be provided");
            if (string.IsNullOrEmpty(targetFolderKey))
                throw new ArgumentNullException(nameof(targetFolderKey));
            await CheckSessionToken();

            var req = new FolderCopyRequest()
            {
                SessionToken = _sessionToken,
                FolderKeySrc = folderKeys != null ? string.Join(",", folderKeys) : null,
                FolderPath = folderPath,
                FolderKeyDst = targetFolderKey
            };
            var res = await GetApiResponse(GetApiUri("folder/move.php"), ToFormUrlEncodedContent(req));
            if (!res.IsSuccessStatusCode)
                throw new Exception(res.Content);

            var jsonRes = JsonConvert.DeserializeObject<ResponseModel<FolderDeleteResponse>>(res.Content);
            CheckApiResponse(jsonRes, "Cannot move folder");

            return true;
        }

        /// <summary>
        /// Update a folder information.
        /// </summary>
        /// <param name="folderKey">Folder key</param>
        /// <param name="folderPath">Folder path</param>
        /// <param name="name">The folder name</param>
        /// <param name="description">The folder description</param>
        /// <param name="privacy">The folder <see cref="Privacy"/></param>
        /// <param name="privacyRecursive">Whether or not applying 'privacy' to sub-folders</param>
        /// <returns>True on success</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="Exception"></exception>
        public async Task<bool> FolderUpdate(string folderKey = null, string folderPath = null, string name = null, string description = null, Privacy? privacy = null, YesNo? privacyRecursive = null)
        {
            if (string.IsNullOrEmpty(folderKey))
                throw new ArgumentNullException(nameof(folderKey));
            await CheckSessionToken();

            var req = new FolderUpdateRequest()
            {
                SessionToken = _sessionToken,
                FolderKey = folderKey,
                FolderPath = folderPath,
                FolderName = name,
                Description = description,
                Privacy = privacy,
                PrivacyRecursive = privacyRecursive
            };
            var res = await GetApiResponse(GetApiUri("folder/update.php"), ToFormUrlEncodedContent(req));
            if (!res.IsSuccessStatusCode)
                throw new Exception(res.Content);

            var jsonRes = JsonConvert.DeserializeObject<ResponseModel<FolderDeleteResponse>>(res.Content);
            CheckApiResponse(jsonRes, "Cannot update folder");

            return true;
        }
    }
}