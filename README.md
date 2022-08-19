# MediaFireApi
[![CodeFactor](https://www.codefactor.io/repository/github/sakya/cmdlineargsparser/badge)](https://www.codefactor.io/repository/github/sakya/mediafireapi)
[![NuGet](https://img.shields.io/nuget/v/mediafireapi.svg)](https://www.nuget.org/packages/mediafireapi/)
[![License](https://img.shields.io/github/license/sakya/mediafireapi)](https://github.com/sakya/mediafireapi/blob/master/LICENSE)

Implementation of the MediaFire API.

https://www.mediafire.com/developers/core_api/1.5/

Basic example:
```csharp
using var client = new Client(new ClientSettings());
// Login
await client.Login("yourAccountEmail", "yourAccountPassword");

// Get user info
var userInfo = await client.UserGetInfo();
// Get folder content
var folderContent = await client.FolderGetContent(Client.RootFolderKey, contentType: FolderContentType.Folders);
// Create a folder
var newFolderKey = await client.FolderCreate(Client.RootFolderKey, name: "test");
// Delete a folder
var folderDelete = await client.FolderDelete(new[] { newFolderKey });
// Purge a folder
await client.FolderPurge(new[] { newFolderKey });
// Get file info
var info = (await client.FileGetInfo(filePath: "/Documents/test.mp3"))?.ToList();
// Get file direct download link
var links = await client.DownloadDirectLink(new[] { info[0].QuickKey });
var url = links.First().DirectDownload;
// Upload a file
using (var fs = new FileStream("test.mp3", FileMode.Open, FileAccess.Read)) {
    await client.UploadSimple(fs, "audio/mpeg", "test.mp3", fs.Length, path: "/Music");
}

// Logout
await client.Logout();
```