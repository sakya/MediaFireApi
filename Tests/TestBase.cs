using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace Tests;

public class TestBase
{
    protected string UserEmail;
    protected string Password;

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
}