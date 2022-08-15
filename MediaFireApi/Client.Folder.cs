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
        public async Task<FolderItem> FolderGetInfo(string folderKey)
        {
            var res = await FolderGetInfo(new[] { folderKey });
            return res.Count > 0 ? res[0] : null;
        }

        public async Task<List<FolderItem>> FolderGetInfo(IEnumerable<string> folderKeys)
        {
            if (folderKeys == null)
                throw new ArgumentNullException(nameof(folderKeys));
            await CheckSessionToken();

            var req = new FolderInfoRequest()
            {
                SessionToken = _sessionToken,
                FolderKey = string.Join("," ,folderKeys)
            };
            var res = await _client.PostAsync(GetApiUri("folder/get_info.php"), ToFormUrlEncodedContent(req));
            var resContent = await res.Content.ReadAsStringAsync();
            if (!res.IsSuccessStatusCode)
                throw new Exception(resContent);

            var jsonRes = JsonConvert.DeserializeObject<ResponseModel<FolderInfoResponse>>(resContent);
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
            var res = await _client.PostAsync(GetApiUri("folder/get_content.php"), ToFormUrlEncodedContent(req));
            var resContent = await res.Content.ReadAsStringAsync();
            if (!res.IsSuccessStatusCode)
                throw new Exception(resContent);

            var jsonRes = JsonConvert.DeserializeObject<ResponseModel<FolderContentResponse>>(resContent);
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
            var res = await _client.PostAsync(GetApiUri("folder/create.php"), ToFormUrlEncodedContent(req));
            var resContent = await res.Content.ReadAsStringAsync();
            if (!res.IsSuccessStatusCode)
                throw new Exception(resContent);

            var jsonRes = JsonConvert.DeserializeObject<ResponseModel<FolderCreateResponse>>(resContent);
            CheckApiResponse(jsonRes, "Cannot create folder");

            return jsonRes?.Response.FolderKey;
        }

        /// <summary>
        /// Move a folder to the trash can
        /// </summary>
        /// <param name="folderKey">The folder key</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="Exception"></exception>
        public Task<bool> FolderDelete(string folderKey)
        {
            return FolderDelete(new[] { folderKey });
        }


        /// <summary>
        /// Move folders to the trash can
        /// </summary>
        /// <param name="folderKeys">Folder keys</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="Exception"></exception>
        public async Task<bool> FolderDelete(IEnumerable<string> folderKeys)
        {
            if (folderKeys == null)
                throw new ArgumentNullException(nameof(folderKeys));
            await CheckSessionToken();

            var req = new FolderDeleteRequest()
            {
                SessionToken = _sessionToken,
                FolderKey = string.Join(",", folderKeys)
            };
            var res = await _client.PostAsync(GetApiUri("folder/delete.php"), ToFormUrlEncodedContent(req));
            var resContent = await res.Content.ReadAsStringAsync();
            if (!res.IsSuccessStatusCode)
                throw new Exception(resContent);

            var jsonRes = JsonConvert.DeserializeObject<ResponseModel<FolderDeleteResponse>>(resContent);
            CheckApiResponse(jsonRes, "Cannot delete folder");

            return true;
        }

        /// <summary>
        /// Delete a folder permanently
        /// </summary>
        /// <param name="folderKey">The folder key</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="Exception"></exception>
        public Task<bool> FolderPurge(string folderKey)
        {
            return FolderPurge(new[] { folderKey });
        }

        /// <summary>
        /// Delete folders permanently
        /// </summary>
        /// <param name="folderKeys">Folder keys</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="Exception"></exception>
        public async Task<bool> FolderPurge(IEnumerable<string> folderKeys)
        {
            if (folderKeys == null)
                throw new ArgumentNullException(nameof(folderKeys));
            await CheckSessionToken();

            var req = new FolderDeleteRequest()
            {
                SessionToken = _sessionToken,
                FolderKey = string.Join(",", folderKeys)
            };
            var res = await _client.PostAsync(GetApiUri("folder/purge.php"), ToFormUrlEncodedContent(req));
            var resContent = await res.Content.ReadAsStringAsync();
            if (!res.IsSuccessStatusCode)
                throw new Exception(resContent);

            var jsonRes = JsonConvert.DeserializeObject<ResponseModel<FolderDeleteResponse>>(resContent);
            CheckApiResponse(jsonRes, "Cannot purge folder");

            return true;
        }

        /// <summary>
        /// Copy folders to a target folder
        /// </summary>
        /// <param name="folderKeys">Folder keys</param>
        /// <param name="targetFolderKey">The target folder key</param>
        /// <returns>The keys of the newly created folders</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="Exception"></exception>
        public async Task<string[]> FolderCopy(IEnumerable<string> folderKeys, string targetFolderKey)
        {
            if (folderKeys == null)
                throw new ArgumentNullException(nameof(folderKeys));
            if (string.IsNullOrEmpty(targetFolderKey))
                throw new ArgumentNullException(nameof(targetFolderKey));
            await CheckSessionToken();

            var req = new FolderCopyRequest()
            {
                SessionToken = _sessionToken,
                FolderKeySrc = string.Join(",", folderKeys),
                FolderKeyDst = targetFolderKey
            };
            var res = await _client.PostAsync(GetApiUri("folder/copy.php"), ToFormUrlEncodedContent(req));
            var resContent = await res.Content.ReadAsStringAsync();
            if (!res.IsSuccessStatusCode)
                throw new Exception(resContent);

            var jsonRes = JsonConvert.DeserializeObject<ResponseModel<FolderCopyResponse>>(resContent);
            CheckApiResponse(jsonRes, "Cannot copy folder");

            return jsonRes?.Response?.NewFolderKey.Split(',');
        }

        /// <summary>
        /// Move folders to a target folder
        /// </summary>
        /// <param name="folderKeys">Folder keys</param>
        /// <param name="targetFolderKey">The target folder key</param>
        /// <returns>True on success</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="Exception"></exception>
        public async Task<bool> FolderMove(IEnumerable<string> folderKeys, string targetFolderKey)
        {
            if (folderKeys == null)
                throw new ArgumentNullException(nameof(folderKeys));
            if (string.IsNullOrEmpty(targetFolderKey))
                throw new ArgumentNullException(nameof(targetFolderKey));
            await CheckSessionToken();

            var req = new FolderCopyRequest()
            {
                SessionToken = _sessionToken,
                FolderKeySrc = string.Join(",", folderKeys),
                FolderKeyDst = targetFolderKey
            };
            var res = await _client.PostAsync(GetApiUri("folder/move.php"), ToFormUrlEncodedContent(req));
            var resContent = await res.Content.ReadAsStringAsync();
            if (!res.IsSuccessStatusCode)
                throw new Exception(resContent);

            var jsonRes = JsonConvert.DeserializeObject<ResponseModel<FolderDeleteResponse>>(resContent);
            CheckApiResponse(jsonRes, "Cannot move folder");

            return true;
        }

        /// <summary>
        /// Update a folder information.
        /// </summary>
        /// <param name="folderKey">Folder key</param>
        /// <param name="name">The folder name</param>
        /// <param name="description">The folder description</param>
        /// <param name="privacy">The folder <see cref="Privacy"/></param>
        /// <param name="privacyRecursive">Whether or not applying 'privacy' to sub-folders</param>
        /// <returns>True on success</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="Exception"></exception>
        public async Task<bool> FolderUpdate(string folderKey, string name = null, string description = null, Privacy? privacy = null, YesNo? privacyRecursive = null)
        {
            if (string.IsNullOrEmpty(folderKey))
                throw new ArgumentNullException(nameof(folderKey));
            await CheckSessionToken();

            var req = new FolderUpdateRequest()
            {
                SessionToken = _sessionToken,
                FolderKey = folderKey,

                FolderName = name,
                Description = description,
                Privacy = privacy,
                PrivacyRecursive = privacyRecursive
            };
            var res = await _client.PostAsync(GetApiUri("folder/update.php"), ToFormUrlEncodedContent(req));
            var resContent = await res.Content.ReadAsStringAsync();
            if (!res.IsSuccessStatusCode)
                throw new Exception(resContent);

            var jsonRes = JsonConvert.DeserializeObject<ResponseModel<FolderDeleteResponse>>(resContent);
            CheckApiResponse(jsonRes, "Cannot update folder");

            return true;
        }
    }
}