using NSwag;
using NSwag.CodeGeneration.CSharp;

class Program
{
    static async Task Main(string[] args)
    {
        // Path to the local swagger.json file
        var swaggerJsonPath = "src/swagger.json";

        // Check if the swagger.json file exists
        if (!File.Exists(swaggerJsonPath))
        {
            Console.WriteLine($"Error: swagger.json not found at {swaggerJsonPath}");
            return;
        }

        // Read the swagger.json file
        var swaggerJson = await File.ReadAllTextAsync(swaggerJsonPath);

        // Generate OpenAPI specification
        var document = await OpenApiDocument.FromJsonAsync(swaggerJson);

        // Configure the code generator settings
        var settings = new CSharpClientGeneratorSettings
        {
            ClassName = "MyWebApiClient",
            CSharpGeneratorSettings =
            {
                Namespace = "MyWeb.Client"
            }
        };

        // Generate the client code
        var generator = new CSharpClientGenerator(document, settings);
        var code = generator.GenerateFile();

        // Write the generated code to a file in the MyClient project
        var outputPath = Path.Combine("src", "MyClient", "MyWebApiClient.cs");
        await File.WriteAllTextAsync(outputPath, code);

        Console.WriteLine($"API client generated successfully at {outputPath}");
    }
}
