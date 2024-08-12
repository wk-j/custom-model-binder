# MyWeb API Project

This project is a .NET 8.0 Web API that includes functionality for weather forecasts and file uploads. It also includes a client API generator and a separate client library.

## Project Structure

- `src/MyWeb`: The main Web API project
- `src/AppGenerator`: A console application to generate API client code
- `src/MyClient`: A library project containing the generated API client

## Features

1. Weather Forecast API
2. File Upload API with metadata support
3. Swagger/OpenAPI integration
4. Client API code generation

## Getting Started

### Prerequisites

- .NET 8.0 SDK

### Running the Web API

1. Navigate to the MyWeb project directory:
   ```
   cd src/MyWeb
   ```

2. Run the project:
   ```
   dotnet run
   ```

The API will be available at `https://localhost:5001` (or `http://localhost:5000`).

### Generating Client API

1. Build the MyWeb project to generate the swagger.json file:
   ```
   dotnet build src/MyWeb
   ```

2. Run the AppGenerator project:
   ```
   dotnet run --project src/AppGenerator
   ```

This will generate the API client code in `src/MyClient/MyWebApiClient.cs`.

## API Endpoints

- GET `/WeatherForecast`: Get a list of weather forecasts
- POST `/Upload`: Upload a file with metadata

## Contributing

Please read [CONTRIBUTING.md](CONTRIBUTING.md) for details on our code of conduct, and the process for submitting pull requests.

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details.
