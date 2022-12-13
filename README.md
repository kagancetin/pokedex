# pokedex
Trial work with Dotnet 7 Api and BlazorWasm,EF, Graphql (Hotchocolate), JWT Authentication

DB: SQL Server

/Server/appsettings.json example

{
  "ConnectionStrings": {
    "ConnectionDefault": "Server=localhost\\sqlexpress;Database=BeastBattle;TrustServerCertificate=true;Trusted_Connection=true"
  },
  "AppSettings": {
    "Token": "My secret"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
