# MyWeb API Project

A .NET 8.0 Web API project with file uploads and client API generation.

## Features
- File Upload API with metadata
- Swagger/OpenAPI integration
- Client API code generation

## Quick Start
1. Run API: `cd src/MyWeb && dotnet run`
2. Generate client: `dotnet build src/MyWeb && dotnet run --project src/AppGenerator`

## API Endpoints
- POST `/Upload`: File upload with metadata

## License
MIT License - see [LICENSE.md](LICENSE.md)
