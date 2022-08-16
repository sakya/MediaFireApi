using System;
using System.Globalization;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
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
        /// Checks if a duplicate filename exists in the destination folder and verifies folder permissions for non-owner uploads
        /// </summary>
        /// <param name="folderKey">The folder key</param>
        /// <param name="fileName">The file name</param>
        /// <param name="size">The file size</param>
        /// <param name="resumable">Specifies whether to make this upload resumable or not</param>
        /// <param name="hash">The SHA256 hash of the file being uploaded</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
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

        /// <summary>
        /// Upload a new file through POST to the user's account
        /// </summary>
        /// <param name="stream">The file stream</param>
        /// <param name="contentType">The file content type</param>
        /// <param name="fileName">The file name</param>
        /// <param name="size">The file size</param>
        /// <param name="folderKey">The folder key</param>
        /// <param name="path">The folder path</param>
        /// <param name="actionOnDuplicate">Specifies the action to take when the file already exists, by name, in the destination folder</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="Exception"></exception>
        public async Task UploadSimple(Stream stream, string contentType, string fileName, long size, string folderKey = null, string path = null, ActionOnDuplicate? actionOnDuplicate = null)
        {
            if (string.IsNullOrEmpty(fileName))
                throw new ArgumentNullException(nameof(fileName));
            await CheckSessionToken();

            using (var client = new HttpClient()) {
                client.DefaultRequestHeaders.Add("x-filename", fileName);
                client.DefaultRequestHeaders.Add("x-filesize", size.ToString(CultureInfo.InvariantCulture));
                using (var multipartFormContent = new MultipartFormDataContent()) {
                    var fileStreamContent = new StreamContent(stream);
                    fileStreamContent.Headers.ContentType = new MediaTypeHeaderValue(contentType);
                    multipartFormContent.Add(fileStreamContent, name: "file", fileName: fileName);
                    using (var res = await client.PostAsync(
                               GetApiUri(
                                   $"upload/simple.php?session_token={_sessionToken}&folder_key={folderKey}&path={path}&action_on_duplicate={actionOnDuplicate.ToString().ToLower()}"),
                               multipartFormContent)) {
                        var resContent = res.Content.ReadAsStringAsync();
                        if (!res.IsSuccessStatusCode)
                            throw new Exception($"Failed to upload file: {resContent}");
                    }
                }
            }
        }
    }
}