# Contributing / Local setup

1. Configure local connection string (do NOT commit):
   - Use `dotnet user-secrets` inside the `Prospera` project folder:
     ```
     cd Prospera
     dotnet user-secrets init
     dotnet user-secrets set "ConnectionStrings:ProsperaContext" "<your-connection-string>"
     ```
   - Or set environment variable `ConnectionStrings__ProsperaContext`.

2. Run the project locally:
   - `dotnet build Prospera/Prospera.csproj`
   - `dotnet run --project Prospera/Prospera.csproj`

3. CI/CD:
   - The GitHub Actions workflow uses secrets `AZURE_WEBAPP_PUBLISH_PROFILE` and `AZURE_SQL_CONNECTIONSTRING`.

Security:
- Rotate any leaked credentials.
- Never commit secrets in `appsettings.*.json`.
