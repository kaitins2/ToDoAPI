# Use .NET 9 SDK for building
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

COPY ToDoAPI/ToDoAPI.csproj ./ToDoAPI/
RUN dotnet restore ToDoAPI/ToDoAPI.csproj

COPY ToDoAPI/. ./ToDoAPI/
WORKDIR /app/ToDoAPI
RUN dotnet publish -c Release -o /app/publish

# Use .NET 9 runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "ToDoAPI.dll"]
