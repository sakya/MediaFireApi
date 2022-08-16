using System.Threading.Tasks;
using MediaFireApi;
using NUnit.Framework;

namespace Tests;

public class File : TestBase
{
    [Test(ExpectedResult = true)]
    public async Task<bool> GetInfo()
    {
        Assert.Pass();
        return true;
    }
}