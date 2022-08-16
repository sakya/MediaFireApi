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
        /// Get the direct link for downloading files
        /// </summary>
        /// <param name="quickKeys">File keys</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="Exception"></exception>
        public async Task<List<DownloadDirectLinkResponse.DirectLink>> DownloadDirectLink(IEnumerable<string> quickKeys = null)
        {
            if (quickKeys == null)
                throw new ArgumentNullException(nameof(quickKeys));
            await CheckSessionToken();

            var req = new DownloadDirectLinkRequest()
            {
                SessionToken = _sessionToken,
                QuickKey = string.Join(",", quickKeys)
            };
            var res = await GetApiResponse(GetApiUri("file/get_links.php"), ToFormUrlEncodedContent(req));
            if (!res.IsSuccessStatusCode)
                throw new Exception(res.Content);

            var jsonRes = JsonConvert.DeserializeObject<ResponseModel<DownloadDirectLinkResponse>>(res.Content);
            CheckApiResponse(jsonRes, "Cannot get direct download link");

            return jsonRes?.Response.Links;
        }
    }
}