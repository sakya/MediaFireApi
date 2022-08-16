using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using MediaFireApi;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Tests;

public class TestBase
{
    protected string UserEmail;
    protected string Password;
    protected Client? _client;

    public TestBase()
    {
        using (var fs = new FileStream("../../../../account-settings.json", FileMode.Open, FileAccess.Read)) {
            using (var sr = new StreamReader(fs)) {
                var accountSettings = JsonConvert.DeserializeObject<Dictionary<string, string>>(sr.ReadToEnd());
                if (accountSettings == null)
                    throw new Exception("Cannot read account settings");

                UserEmail = accountSettings["userEmail"];
                Password = accountSettings["password"];
            }
        }
    }

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
}