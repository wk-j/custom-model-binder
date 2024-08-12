using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Newtonsoft.Json;

namespace ClientTests
{
    public class FileUploadTests
    {
        private const string BaseUrl = "http://localhost:8080";

        [Fact]
        public async Task TestFileUpload()
        {
            using var client = new HttpClient();
            client.BaseAddress = new Uri(BaseUrl);

            var content = new MultipartFormDataContent();

            // Add file content
            var fileContent = new ByteArrayContent(Encoding.UTF8.GetBytes("This is a test file content"));
            content.Add(fileContent, "file", "test.txt");

            // Add other form fields
            content.Add(new StringContent("Test Description"), "description");
            content.Add(new StringContent("TestCategory"), "category");
            content.Add(new StringContent(DateTime.UtcNow.ToString("o")), "upload_date");

            // Add metadata
            var metadata = new Dictionary<string, string>
            {
                { "key1", "value1" },
                { "key2", "value2" }
            };
            content.Add(new StringContent(JsonConvert.SerializeObject(metadata)), "metadata");

            // Send the request
            var response = await client.PostAsync("/upload", content);

            // Assert
            Assert.True(response.IsSuccessStatusCode);

            var responseContent = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<UploadResponse>(responseContent);

            Assert.NotNull(result);
            Assert.Equal("test.txt", result.FileName);
            Assert.Equal("Test Description", result.Description);
            Assert.Equal("TestCategory", result.Category);
            Assert.Equal(2, result.MetadataCount);
        }
    }

    public class UploadResponse
    {
        public string FileName { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string UploadDate { get; set; }
        public int MetadataCount { get; set; }
    }
}
