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
                throw new ArgumentException("quickKeys or filePath must be provided");
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
        /// <param name="quickKeys">Folder keys</param>
        /// <param name="filePath">Folder path</param>
        /// <param name="targetFolderKey">The target folder key</param>
        /// <param name="targetFolderPath">The target folder path</param>
        /// <returns>The keys of the newly created folders</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="Exception"></exception>
        public async Task<string[]> FileCopy(IEnumerable<string> quickKeys = null, string filePath = null, string targetFolderKey = null, string targetFolderPath = null)
        {
            if (quickKeys == null && string.IsNullOrEmpty(filePath))
                throw new ArgumentException("FolderKeys or folderPath must be provided");
            if (string.IsNullOrEmpty(targetFolderKey))
                throw new ArgumentNullException(nameof(targetFolderKey));
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
                throw new ArgumentException("quickKeys or filePath must be provided");
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

            var jsonRes = JsonConvert.DeserializeObject<ResponseModel<FolderDeleteResponse>>(res.Content);
            CheckApiResponse(jsonRes, "Cannot delete file");

            return true;
        }
    }
}