# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy and restore the project
COPY ToDoAPI.csproj ./
RUN dotnet restore

# Copy the rest of the source code
COPY . ./
RUN dotnet publish -c Release -o /app/publish

# Stage 2: Run
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:$PORT
ENTRYPOINT ["dotnet", "ToDoAPI.dll"]
