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


public class UploadTests
{
    [Fact]
    public async Task TestUpload()
    {
        var client = new MyWebApiClient("http://localhost:5000", new System.Net.Http.HttpClient());
        var file = new System.IO.FileStream("test.txt", System.IO.FileMode.Open);
        var result = await client.UploadAsync(file);
        Assert.NotNull(result);
    }
}
