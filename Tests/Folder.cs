using System.Threading.Tasks;
using MediaFireApi;
using NUnit.Framework;

namespace Tests;

public class Folder
{
    private Client? _client;

    [OneTimeSetUp]
    public async void Setup()
    {
        var settings = new ClientSettings();
        _client = new Client(settings);
        await _client.Login("", "");
    }

    [OneTimeTearDown]
    public async void ClassCleanup()
    {
        if (_client != null) {
            await _client.Logout();
            _client.Dispose();
        }
    }

    [Test(ExpectedResult = true)]
    public async Task<bool> GetInfo()
    {
        await _client!.FolderGetInfo("myfiles");

        Assert.Pass();
        return true;
    }
}