using System;
using System.Threading.Tasks;
using MediaFireApi;
using NUnit.Framework;

namespace Tests;

public class Folder : TestBase
{
    private Client? _client;
    private readonly string _testFolderName = Guid.NewGuid().ToString("N");

    [OneTimeSetUp]
    public async Task Setup()
    {
        var settings = new ClientSettings();
        _client = new Client(settings);
        await _client.Login(UserEmail, Password);
    }

    [OneTimeTearDown]
    public async Task ClassCleanup()
    {
        if (_client != null) {
            await _client.Logout();
            _client.Dispose();
        }
    }

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
}