using System.Threading.Tasks;
using MediaFireApi;
using NUnit.Framework;

namespace Tests;

public class Auth
{
    [SetUp]
    public void Setup()
    {
    }

    [Test(ExpectedResult = true)]
    public async Task<bool> Login()
    {
        var settings = new ClientSettings();
        using var client = new Client(settings);
        await client.Login("", "");
        await client.Logout();

        Assert.Pass();
        return true;
    }

    [Test(ExpectedResult = true)]
    public async Task<bool> RenewSession()
    {
        var settings = new ClientSettings();
        using var client = new Client(settings);
        await client.Login("", "");
        await client.RenewSessionToken();
        await client.Logout();

        Assert.Pass();
        return true;
    }
}