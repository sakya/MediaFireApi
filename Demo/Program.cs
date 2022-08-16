using MediaFireApi;
using MediaFireApi.Models;
using Newtonsoft.Json;

// To run the demo program create a file account-settings.json in the solution folder
// {
//   "userEmail": "MediaFireAccountEmail",
//   "password": "MediaFirePassword",
// }
var settings = new ClientSettings();
using var client = new Client(settings);

using (var fs = new FileStream("../../../../account-settings.json", FileMode.Open, FileAccess.Read)) {
    using (var sr = new StreamReader(fs)) {
        var accountSettings = JsonConvert.DeserializeObject<Dictionary<string, string>>(await sr.ReadToEndAsync());
        if (accountSettings == null)
            throw new Exception("Cannot read account settings");
        await client.Login(accountSettings["userEmail"], accountSettings["password"]);
    }
}

await client.FolderGetDepth(folderPath: "/DepthTest/Depth1/Depth2/Depth3");

var uploadCheck = await client.UploadCheck(Client.RootFolderKey, "test.mp4", 8 * 1024 * 1024);
using (var ms = new MemoryStream()) {
    for (int i = 0; i < 1024*1024; i++) {
        ms.Write(new byte[] {0 }, 0, 1);
    }

    ms.Seek(0, SeekOrigin.Begin);
    await client.UploadSimple(ms, "application/octet-stream", "test.bin", ms.Length, path: "/Documents");
}

var userInfo = await client.UserGetInfo();
var folderInfo = await client.FolderGetInfo(new[] { Client.RootFolderKey });
folderInfo = await client.FolderGetInfo(folderPath: "/Documents");
var folderContent = await client.FolderGetContent(Client.RootFolderKey, contentType: FolderContentType.Folders);
if (folderContent.Folders?.Count > 1) {
    var foldersInfo = await client.FolderGetInfo(new[] { folderContent.Folders[0].FolderKey, folderContent.Folders[1].FolderKey });
}
folderContent = await client.FolderGetContent(Client.RootFolderKey, contentType: FolderContentType.Files);
if (folderContent.Files?.Count > 0) {
    var directLinks = await client.DownloadDirectLink(new[] { folderContent.Files[0].QuickKey });
}

var newFolderKey = await client.FolderCreate(Client.RootFolderKey, name: "test");
var folderDelete = await client.FolderDelete(new[] { newFolderKey });
folderDelete = await client.FolderPurge(new[] { newFolderKey });

await client.Logout();
