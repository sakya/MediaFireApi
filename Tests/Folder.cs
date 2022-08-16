using System;
using System.Threading.Tasks;
using MediaFireApi;
using MediaFireApi.Models;
using NUnit.Framework;

namespace Tests;

public class Folder : TestBase
{
    private readonly string _testFolderName = Guid.NewGuid().ToString("N");

    [Test(ExpectedResult = true)]
    public async Task<bool> GetInfo()
    {
        await _client!.FolderGetInfo(new[] { Client.RootFolderKey });

        Assert.Pass();
        return true;
    }

    [Test(ExpectedResult = true)]
    public async Task<bool> GetContent()
    {
        await _client!.FolderGetContent(Client.RootFolderKey);

        Assert.Pass();
        return true;
    }

    [Test(ExpectedResult = true)]
    public async Task<bool> CreateDeleteAndPurge()
    {
        var folderCreate = await _client!.FolderCreate(Client.RootFolderKey, name: _testFolderName);
        await _client!.FolderDelete(new[] { folderCreate });
        await _client!.FolderPurge(new[] { folderCreate });
        Assert.Pass();
        return true;
    }

    [Test(ExpectedResult = true)]
    public async Task<bool> Copy()
    {
        var folderCreate = await _client!.FolderCreate(Client.RootFolderKey, name: _testFolderName);

        var newFolder = Guid.NewGuid().ToString("N");
        var subFolder = await _client!.FolderCreate(folderCreate, name: newFolder);
        await _client!.FolderCopy(new [] { subFolder }, targetFolderKey: folderCreate);

        await _client!.FolderDelete(new[] { folderCreate });
        await _client!.FolderPurge(new[] { folderCreate });

        Assert.Pass();
        return true;
    }

    [Test(ExpectedResult = true)]
    public async Task<bool> Move()
    {
        var folderCreate = await _client!.FolderCreate(Client.RootFolderKey, name: _testFolderName);

        var newFolder = Guid.NewGuid().ToString("N");
        var subFolder = await _client!.FolderCreate(Client.RootFolderKey, name: newFolder);
        await _client!.FolderMove(new [] { subFolder }, targetFolderKey: folderCreate);

        await _client!.FolderDelete(new[] { folderCreate });
        await _client!.FolderPurge(new[] { folderCreate });

        Assert.Pass();
        return true;
    }

    [Test(ExpectedResult = true)]
    public async Task<bool> Update()
    {
        var folderCreate = await _client!.FolderCreate(Client.RootFolderKey, name: _testFolderName);

        await _client.FolderUpdate(folderCreate, name: Guid.NewGuid().ToString("N"), description: "Test description",
            privacy: Privacy.Public);

        await _client!.FolderDelete(new[] { folderCreate });
        await _client!.FolderPurge(new[] { folderCreate });

        Assert.Pass();
        return true;
    }
}