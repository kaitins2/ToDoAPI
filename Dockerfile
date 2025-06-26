# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy csproj and restore
COPY ToDoAPI/ToDoAPI.csproj ./ToDoAPI/
RUN dotnet restore ToDoAPI/ToDoAPI.csproj

# Copy the rest of the code
COPY ToDoAPI/. ./ToDoAPI/

WORKDIR /app/ToDoAPI
RUN dotnet publish -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "ToDoAPI.dll"]
