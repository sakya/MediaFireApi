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

var folderInfo = await client.FolderGetInfo("myfiles");
var folderContent = await client.FolderGetContent("myfiles", FolderContentType.Files);
var newFolderKey = await client.FolderCreate(null, "test");
var folderDelete = await client.FolderDelete(new string[] { newFolderKey });
folderDelete = await client.FolderPurge(new string[] { newFolderKey });


await client.Logout();
