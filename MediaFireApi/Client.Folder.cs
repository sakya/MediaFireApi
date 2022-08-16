using System;
using System.Collections;
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
        /// <summary>
        /// Specifies how deep in the folder structure, how far from root, the target folder is.
        /// The number of levels deep is returned with a list of "chain folders" which illustrate the direct path from root to the target folder.
        /// </summary>
        /// <param name="folderKeys">Folder keys</param>
        /// <param name="folderPath">Folder path</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="Exception"></exception>
        public async Task<FolderDepthResponse.FolderDepthModel> FolderGetDepth(IEnumerable<string> folderKeys = null, string folderPath = null)
        {
            if (folderKeys == null && string.IsNullOrEmpty(folderPath))
                throw new ArgumentException($"{nameof(folderKeys)} or {nameof(folderPath)} must be provided");
            await CheckSessionToken();

            var req = new FolderDeleteRequest()
            {
                SessionToken = _sessionToken,
                FolderKey = folderKeys != null ? string.Join(",", folderKeys) : null,
                FolderPath = folderPath
            };
            var res = await GetApiResponse(GetApiUri("folder/get_depth.php"), ToFormUrlEncodedContent(req));
            if (!res.IsSuccessStatusCode)
                throw new Exception(res.Content);

            var jsonRes = JsonConvert.DeserializeObject<ResponseModel<FolderDepthResponse>>(res.Content);
            CheckApiResponse(jsonRes, "Cannot get folder depth");

            return jsonRes?.Response.FolderDepth;
        }

        /// <summary>
        /// Returns a list of a folder's details.
        /// </summary>
        /// <param name="folderKeys">Folder keys</param>
        /// <param name="folderPath">Folder path</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="Exception"></exception>
        public async Task<List<FolderItem>> FolderGetInfo(IEnumerable<string> folderKeys = null, string folderPath = null)
        {
            if (folderKeys == null && string.IsNullOrEmpty(folderPath))
                throw new ArgumentException($"{nameof(folderKeys)} or {nameof(folderPath)} must be provided");
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

        /// <summary>
        /// Returns a collection of top-level folders or files for target folder.
        /// </summary>
        /// <param name="folderKey">The folder key</param>
        /// <param name="folderPath">Folder path</param>
        /// <param name="contentType">Specifies the type of content to return.</param>
        /// <param name="chunk">Specifies which segment of the results to return starting from 1</param>
        /// <param name="chunkSize">The number of items to include in each chunk returned. Range: 100 to 1000. Default: 100. </param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="Exception"></exception>
        public async Task<FolderContentResponse.FolderContentModel> FolderGetContent(string folderKey = null, string folderPath = null, FolderContentType contentType = FolderContentType.Files, int chunk = 1, int chunkSize = 100)
        {
            if (string.IsNullOrEmpty(folderKey) && string.IsNullOrEmpty(folderPath))
                throw new ArgumentException($"{nameof(folderKey)} or {nameof(folderPath)} must be provided");
            await CheckSessionToken();

            var req = new FolderContentRequest()
            {
                SessionToken = _sessionToken,
                FolderKey = folderKey,
                FolderPath = folderPath,

                ContentType = contentType,
                ChunkSize = chunkSize,
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
        /// Creates a folder in a specified target destination.
        /// </summary>
        /// <param name="parentKey">The parent folder key</param>
        /// <param name="parentPath">The parent folder path</param>
        /// <param name="name">The new folder name</param>
        /// <param name="actionOnDuplicate">The <see cref="ActionOnDuplicate"/></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="Exception"></exception>
        public async Task<string> FolderCreate(string parentKey = null, string parentPath = null, string name = null, ActionOnDuplicate actionOnDuplicate = ActionOnDuplicate.Keep)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));
            await CheckSessionToken();

            var req = new FolderCreateRequest()
            {
                SessionToken = _sessionToken,
                ParentKey = parentKey,
                ParentPath = parentPath,
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
        /// Deletes one or more session user's folders by setting the folders' and their contents' delete_date properties and moving the folders and their contents to the trash can.
        /// The folder is not deleted permanently but, rather, the folder is moved to the trash can.
        /// </summary>
        /// <param name="folderKeys">Folder keys</param>
        /// <param name="folderPath">Folder path</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="Exception"></exception>
        public async Task<bool> FolderDelete(IEnumerable<string> folderKeys = null, string folderPath = null)
        {
            if (folderKeys == null && string.IsNullOrEmpty(folderPath))
                throw new ArgumentException($"{nameof(folderKeys)} or {nameof(folderPath)} must be provided");
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
        /// Deletes one or more of a session user's folders permanently, along with all contents of the folders, by removing their entries from the database.
        /// </summary>
        /// <remarks>THIS OPTION CANNOT BE UNDONE</remarks>
        /// <param name="folderKeys">Folder keys</param>
        /// <param name="folderPath">Folder path</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="Exception"></exception>
        public async Task<bool> FolderPurge(IEnumerable<string> folderKeys = null, string folderPath = null)
        {
            if (folderKeys == null && string.IsNullOrEmpty(folderPath))
                throw new ArgumentException($"{nameof(folderKeys)} or {nameof(folderPath)} must be provided");
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
        /// Copies a session user's folder and its children to a target destination.
        /// </summary>
        /// <param name="folderKeys">Folder keys</param>
        /// <param name="folderPath">Folder path</param>
        /// <param name="targetFolderKey">The target folder key</param>
        /// <param name="targetFolderPath">The target folder path</param>
        /// <returns>The keys of the newly created folders</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="Exception"></exception>
        public async Task<IEnumerable<string>> FolderCopy(IEnumerable<string> folderKeys = null, string folderPath = null, string targetFolderKey = null, string targetFolderPath = null)
        {
            if (folderKeys == null && string.IsNullOrEmpty(folderPath))
                throw new ArgumentException($"{nameof(folderKeys)} or {nameof(folderPath)} must be provided");
            if (string.IsNullOrEmpty(targetFolderKey) && string.IsNullOrEmpty(targetFolderPath))
                throw new ArgumentException($"{nameof(targetFolderKey)} or ${nameof(targetFolderPath)} must be provided");
            await CheckSessionToken();

            var req = new FolderCopyRequest()
            {
                SessionToken = _sessionToken,
                FolderKeySrc = folderKeys != null ? string.Join(",", folderKeys) : null,
                FolderPathSrc = folderPath,
                FolderKeyDst = targetFolderKey,
                FolderPathDst = targetFolderPath
            };
            var res = await GetApiResponse(GetApiUri("folder/copy.php"), ToFormUrlEncodedContent(req));
            if (!res.IsSuccessStatusCode)
                throw new Exception(res.Content);

            var jsonRes = JsonConvert.DeserializeObject<ResponseModel<FolderCopyResponse>>(res.Content);
            CheckApiResponse(jsonRes, "Cannot copy folder");

            return jsonRes?.Response?.NewFolderKey;
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
        public async Task<bool> FolderMove(IEnumerable<string> folderKeys = null, string folderPath = null, string targetFolderKey = null, string targetFolderPath = null)
        {
            if (folderKeys == null && string.IsNullOrEmpty(folderPath))
                throw new ArgumentException($"{nameof(folderKeys)} or {nameof(folderPath)} must be provided");
            if (string.IsNullOrEmpty(targetFolderKey) && string.IsNullOrEmpty(targetFolderPath))
                throw new ArgumentException($"{nameof(targetFolderKey)} or ${nameof(targetFolderPath)} must be provided");
            await CheckSessionToken();

            var req = new FolderCopyRequest()
            {
                SessionToken = _sessionToken,
                FolderKeySrc = folderKeys != null ? string.Join(",", folderKeys) : null,
                FolderPathSrc = folderPath,
                FolderKeyDst = targetFolderKey,
            };
            var res = await GetApiResponse(GetApiUri("folder/move.php"), ToFormUrlEncodedContent(req));
            if (!res.IsSuccessStatusCode)
                throw new Exception(res.Content);

            var jsonRes = JsonConvert.DeserializeObject<ResponseModel<FolderMoveResponse>>(res.Content);
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