# DevTalensGraphQLWorkshop

## Getting started

In the instructions you received it was mentioned to have a local instance (docker, installed) of SQL server running and that you should have an empty database and the connection string to that database.

For every example set the connection string to your local database via the appsettings.json

Install entity framework core tools https://learn.microsoft.com/en-us/ef/core/cli/dotnet

Migrate the database by running below in the folder containing the SweetLemons csproj file

```
dotnet ef database update
```
