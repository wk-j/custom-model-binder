using Xunit;
using MyWeb.Client;

namespace ClientTests;

public class MyWebApiClientTests
{
    [Fact]
    public void TestMyWebApiClientCreation()
    {
        var client = new MyWebApiClient("http://localhost:5000", new System.Net.Http.HttpClient());
        Assert.NotNull(client);
    }
}
