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
        /// Returns the trash can folder data and the list of immediate files and folders in the trash can.
        /// Contents of subfolders in the trash can will not be returned.
        /// </summary>
        /// <param name="deviceId">The device id</param>
        /// <param name="contentType">The <see cref="FolderContentType"/></param>
        /// <param name="chunk">The chunk</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<IEnumerable<Item>> DeviceGetTrash(string deviceId = null, FolderContentType contentType = FolderContentType.Files, int chunk = 1)
        {
            await CheckSessionToken();

            var req = new DeviceGetTrashApiRequest()
            {
                SessionToken = _sessionToken,
                DeviceId = deviceId,
                ContentType = contentType,
                Chunk = chunk
            };
            var res = await GetApiResponse(GetApiUri("device/get_trash.php"), ToFormUrlEncodedContent(req));
            var jsonRes = JsonConvert.DeserializeObject<ResponseModel<DeviceGetTrashResponse>>(res.Content);
            CheckApiResponse(jsonRes, "Cannot get trash");

            if (jsonRes?.Response.Files != null)
                return jsonRes.Response.Files;
            return jsonRes?.Response.Folders;
        }

        /// <summary>
        /// Empties out the content of the trash can.
        /// If the trash has too many items, then the API will return immediately with the property asynchronous=yes and the operation will be performed in the background.
        /// </summary>
        /// <param name="deviceId">The device id</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<bool> DeviceEmptyTrash(string deviceId = null)
        {
            await CheckSessionToken();

            var req = new DeviceEmptyTrashRequest()
            {
                SessionToken = _sessionToken,
                DeviceId = deviceId,
            };
            var res = await GetApiResponse(GetApiUri("device/empty_trash.php"), ToFormUrlEncodedContent(req));
            var jsonRes = JsonConvert.DeserializeObject<ResponseModel<DeviceEmptyTrashResponse>>(res.Content);
            CheckApiResponse(jsonRes, "Cannot empty trash");

            return true;
        }
    }
}