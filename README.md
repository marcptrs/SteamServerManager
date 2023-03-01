# SteamServerManager
A web based front end for SteamCMD to easily add and maintain your game servers.

## Getting Started
The solution requires the latest version of [.NET 7 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/7.0)

Once installed, you can open the solution in the .NET IDE of your choice (Visual Studio/Rider/etc)

Launch the app:
```bash
cd SteamServerManager\src\WebUI\Server
dotnet run
```

## Database
### Configuration
The project is currently configured to use [Entity Framework Core with Sqlite](https://learn.microsoft.com/en-us/ef/core/get-started/overview/first-app?tabs=netcore-cli)
### Migrations
The project uses Entity Framework Core and migrations can be run using the EF Core CLI Tools. Install the tools using the following command:

```bash
dotnet tool install --global dotnet-ef
```

Once installed, create a new migration with the following commands:

```bash
cd src\Infrastructure
dotnet ef migrations add "Initial" --startup-project ..\WebUI\Server -o Data\
```

To update the database:

```bash
cd src\Infrastructure
dotnet ef database update --startup-project ..\WebUI\Server
```