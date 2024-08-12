# MyWeb API Project

A .NET 8.0 Web API project with file uploads and client API generation.

## Features
- File Upload API with metadata
- Custom Model Binding for file uploads
- Swagger/OpenAPI integration
- Client API code generation

## Quick Start
1. Run API: `cd src/MyWeb && dotnet run`
2. Generate client: `dotnet build src/MyWeb && dotnet run --project src/AppGenerator`

## API Endpoints
- POST `/Upload`: File upload with metadata

## Custom Model Binding

This project uses a custom `FileUploadModelBinder` to handle file uploads with additional metadata. Here's how it works:

1. The `FileUploadModelBinder` implements `IModelBinder` interface.
2. It binds incoming multipart form data to the `FileUploadModel` class.
3. The binder extracts:
   - File content
   - Description (string)
   - Category (string)
   - Upload date (DateTime)
   - Metadata (Dictionary<string, string>)
4. It handles potential JSON deserialization for the metadata field.
5. The binder allows for flexible and type-safe handling of complex file upload requests.

This approach provides a clean separation of concerns and makes it easy to handle complex file upload scenarios with additional metadata.
