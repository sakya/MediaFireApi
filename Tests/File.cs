using System;
using System.IO;
using System.Threading.Tasks;
using MediaFireApi;
using NUnit.Framework;

namespace Tests;

public class File : TestBase
{
    private readonly string _testFileName = Guid.NewGuid().ToString("N");

    [Test(ExpectedResult = true)]
    public async Task<bool> GetInfo()
    {
        await CreateTestFile();
        await _client.FileGetInfo(filePath: $"/{_testFileName}");
        await _client.FileDelete(filePath: $"/{_testFileName}");
        Assert.Pass();
        return true;
    }

    private async Task CreateTestFile()
    {
        using (var ms = new MemoryStream()) {
            for (int i = 0; i < 128 * 1024; i++) {
                ms.Write(new byte[] { 0 }, 0, 1);
            }

            ms.Seek(0, SeekOrigin.Begin);
            await _client.UploadSimple(ms, "application/octet-stream", _testFileName, ms.Length,
                folderKey: Client.RootFolderKey);
        }
    }
}