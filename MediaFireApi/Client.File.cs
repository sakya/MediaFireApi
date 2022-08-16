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
        /// <summary>
        /// Get files info
        /// </summary>
        /// <param name="quickKeys">File keys</param>
        /// <param name="filePath">File path</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="Exception"></exception>
        public async Task<List<FileItem>> FileGetInfo(IEnumerable<string> quickKeys = null, string filePath = null)
        {
            if (quickKeys == null && string.IsNullOrEmpty(filePath))
                throw new ArgumentException($"{nameof(quickKeys)} or {nameof(filePath)} must be provided");
            await CheckSessionToken();

            var req = new FileInfoRequest()
            {
                SessionToken = _sessionToken,
                QuickKey = quickKeys != null ? string.Join(",", quickKeys) : null,
                FilePath = filePath
            };
            var res = await GetApiResponse(GetApiUri("file/get_info.php"), ToFormUrlEncodedContent(req));
            if (!res.IsSuccessStatusCode)
                throw new Exception(res.Content);

            var jsonRes = JsonConvert.DeserializeObject<ResponseModel<FileInfoResponse>>(res.Content);
            CheckApiResponse(jsonRes, "Cannot get file info");

            if (jsonRes?.Response.FileInfos == null && jsonRes?.Response.FileItemInfo != null)
                return new List<FileItem>() { jsonRes.Response.FileItemInfo };
            return jsonRes?.Response.FileInfos;
        }

        /// <summary>
        /// Copy files to a target folder
        /// </summary>
        /// <param name="quickKeys">File keys</param>
        /// <param name="filePath">File path</param>
        /// <param name="targetFolderKey">The target folder key</param>
        /// <param name="targetFolderPath">The target folder path</param>
        /// <returns>The keys of the newly created folders</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="Exception"></exception>
        public async Task<string[]> FileCopy(IEnumerable<string> quickKeys = null, string filePath = null, string targetFolderKey = null, string targetFolderPath = null)
        {
            if (quickKeys == null && string.IsNullOrEmpty(filePath))
                throw new ArgumentException($"{nameof(quickKeys)} or {nameof(filePath)} must be provided");
            if (string.IsNullOrEmpty(targetFolderKey) && string.IsNullOrEmpty(targetFolderPath))
                throw new ArgumentException($"{nameof(targetFolderKey)} or ${nameof(targetFolderPath)} must be provided");
            await CheckSessionToken();

            var req = new FileCopyRequest()
            {
                SessionToken = _sessionToken,
                QuickKey = quickKeys != null ? string.Join(",", quickKeys) : null,
                FilePath = filePath,
                FolderKey = targetFolderKey,
                FolderPath = targetFolderPath
            };
            var res = await GetApiResponse(GetApiUri("file/copy.php"), ToFormUrlEncodedContent(req));
            if (!res.IsSuccessStatusCode)
                throw new Exception(res.Content);

            var jsonRes = JsonConvert.DeserializeObject<ResponseModel<FileCopyResponse>>(res.Content);
            CheckApiResponse(jsonRes, "Cannot copy file");

            return jsonRes?.Response?.NewQuickKey.Split(',');
        }

        public async Task<FileCreateResponse> FileCreate(string parentKey = null, string parentPath = null, string fileName = null)
        {
            if (string.IsNullOrEmpty(parentKey) && string.IsNullOrEmpty(parentPath))
                throw new ArgumentException($"{nameof(parentKey)} or {nameof(parentPath)} must be provided");
            await CheckSessionToken();

            var req = new FileCreateRequest()
            {
                SessionToken = _sessionToken,
                FileName = fileName,
                ParentKey = parentKey,
                ParentPath = parentPath
            };
            var res = await GetApiResponse(GetApiUri("file/create.php"), ToFormUrlEncodedContent(req));
            if (!res.IsSuccessStatusCode)
                throw new Exception(res.Content);

            var jsonRes = JsonConvert.DeserializeObject<ResponseModel<FileCreateResponse>>(res.Content);
            CheckApiResponse(jsonRes, "Cannot create file");

            return jsonRes?.Response;
        }

        /// <summary>
        /// Move files to the trash can
        /// </summary>
        /// <param name="quickKeys">File keys</param>
        /// <param name="filePath">File path</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="Exception"></exception>
        public async Task<bool> FileDelete(IEnumerable<string> quickKeys = null, string filePath = null)
        {
            if (quickKeys == null && string.IsNullOrEmpty(filePath))
                throw new ArgumentException($"{nameof(quickKeys)} or {nameof(filePath)} must be provided");
            await CheckSessionToken();

            var req = new FileDeleteRequest()
            {
                SessionToken = _sessionToken,
                QuickKey = quickKeys != null ? string.Join(",", quickKeys) : null,
                FilePath = filePath
            };
            var res = await GetApiResponse(GetApiUri("file/delete.php"), ToFormUrlEncodedContent(req));
            if (!res.IsSuccessStatusCode)
                throw new Exception(res.Content);

            var jsonRes = JsonConvert.DeserializeObject<ResponseModel<FileDeleteResponse>>(res.Content);
            CheckApiResponse(jsonRes, "Cannot delete file");

            return true;
        }

        /// <summary>
        /// Move files to a target folder
        /// </summary>
        /// <param name="quickKeys">File keys</param>
        /// <param name="filePath">File path</param>
        /// <param name="targetFolderKey">The target folder key</param>
        /// <param name="targetFolderPath">The target folder path</param>
        /// <returns>True on success</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="Exception"></exception>
        public async Task<bool> FileMove(IEnumerable<string> quickKeys = null, string filePath = null, string targetFolderKey = null, string targetFolderPath = null)
        {
            if (quickKeys == null && string.IsNullOrEmpty(filePath))
                throw new ArgumentException($"{nameof(quickKeys)} or {nameof(filePath)} must be provided");
            if (string.IsNullOrEmpty(targetFolderKey) && string.IsNullOrEmpty(targetFolderPath))
                throw new ArgumentException($"{nameof(targetFolderKey)} or ${nameof(targetFolderPath)} must be provided");
            await CheckSessionToken();

            var req = new FileCopyRequest()
            {
                SessionToken = _sessionToken,
                QuickKey = quickKeys != null ? string.Join(",", quickKeys) : null,
                FilePath = filePath,
                FolderKey = targetFolderKey,
                FolderPath = targetFolderPath
            };
            var res = await GetApiResponse(GetApiUri("file/move.php"), ToFormUrlEncodedContent(req));
            if (!res.IsSuccessStatusCode)
                throw new Exception(res.Content);

            var jsonRes = JsonConvert.DeserializeObject<ResponseModel<FolderDeleteResponse>>(res.Content);
            CheckApiResponse(jsonRes, "Cannot move file");

            return true;
        }

        /// <summary>
        /// Delete files permanently
        /// </summary>
        /// <param name="quickKeys">File keys</param>
        /// <param name="filePath">File path</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="Exception"></exception>
        public async Task<bool> FilePurge(IEnumerable<string> quickKeys = null, string filePath = null)
        {
            if (quickKeys == null && string.IsNullOrEmpty(filePath))
                throw new ArgumentException($"{nameof(quickKeys)} or {nameof(filePath)} must be provided");
            await CheckSessionToken();

            var req = new FileDeleteRequest()
            {
                SessionToken = _sessionToken,
                QuickKey = quickKeys != null ? string.Join(",", quickKeys) : null,
                FilePath = filePath
            };
            var res = await GetApiResponse(GetApiUri("file/purge.php"), ToFormUrlEncodedContent(req));
            if (!res.IsSuccessStatusCode)
                throw new Exception(res.Content);

            var jsonRes = JsonConvert.DeserializeObject<ResponseModel<FileDeleteResponse>>(res.Content);
            CheckApiResponse(jsonRes, "Cannot purge file");

            return true;
        }

        /// <summary>
        /// Update a folder information.
        /// </summary>
        /// <param name="quickKey">File key</param>
        /// <param name="filePath">File path</param>
        /// <param name="filename">The file name</param>
        /// <param name="description">The file description</param>
        /// <param name="privacy">The file <see cref="Privacy"/></param>
        /// <param name="truncate">Specifies if the content of the file should be deleted. no(default) or yes.</param>
        /// <returns>True on success</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="Exception"></exception>
        public async Task<bool> FileUpdate(string quickKey = null, string filePath = null, string filename = null,
            string description = null, YesNo? truncate = null, Privacy? privacy = null)
        {
            if (string.IsNullOrEmpty(quickKey))
                throw new ArgumentNullException(nameof(quickKey));
            await CheckSessionToken();

            var req = new FileUpdateRequest()
            {
                SessionToken = _sessionToken,
                QuickKey = quickKey,
                FilePath = filePath,
                FileName = filename,
                Description = description,
                Privacy = privacy,
                Truncate = truncate
            };
            var res = await GetApiResponse(GetApiUri("file/update.php"), ToFormUrlEncodedContent(req));
            if (!res.IsSuccessStatusCode)
                throw new Exception(res.Content);

            var jsonRes = JsonConvert.DeserializeObject<ResponseModel<FolderDeleteResponse>>(res.Content);
            CheckApiResponse(jsonRes, "Cannot update file");

            return true;
        }
    }
}