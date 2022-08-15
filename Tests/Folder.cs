using System.Threading.Tasks;
using MediaFireApi;
using NUnit.Framework;

namespace Tests;

public class Folder : TestBase
{
    private Client? _client;

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
        await _client!.FolderGetInfo("myfiles");

        Assert.Pass();
        return true;
    }
}