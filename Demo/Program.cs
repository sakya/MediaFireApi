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

var createFile = await client.FileCreate(Client.RootFolderKey, fileName: "test.txt");
var uploadCheck = await client.UploadCheck(Client.RootFolderKey, "test.mp4", 8 * 1024 * 1024);

await client.Logout();
