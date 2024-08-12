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
        var file = new FileParameter(new System.IO.FileStream("../../../../../README.md", System.IO.FileMode.Open), "test.txt", "text/plain");
        try
        {
            await client.UploadAsync(
                file: file,
                description: "Test Description",
                category: "TestCategory",
                uploadDate: DateTimeOffset.Now,
                metadata: new Dictionary<string, string>() {
                    { "key1", "value1" },
                    { "key2", "value2" },
                    { "key3", "value3" }
                }
            );
        }
        catch (MyWeb.Client.ApiException ex)
        {
            Console.WriteLine($"API Exception: {ex.Message}");
            Console.WriteLine($"Status: {ex.StatusCode}");
            Console.WriteLine($"Response: {ex.Response}");
            throw;
        }
    }
}
