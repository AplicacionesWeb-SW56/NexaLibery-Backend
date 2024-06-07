FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /App

env MYSQL_HOST=
env MYSQL_PORT=
env MYSQL_USER=
env MYSQL_PASSWORD=
env MYSQL_DATABASE=

# Copy everything
COPY . ./
# Restore as distinct layers
RUN dotnet restore
# Build and publish a release
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /App
COPY --from=build-env /App/out .
CMD ["dotnet", "NexaLibery-Backend.API.dll"]
