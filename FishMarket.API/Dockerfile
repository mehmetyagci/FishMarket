# FishMarket.API/Dockerfile
# Base image
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

# Publish API project
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY FishMarket.API/FishMarket.API.csproj FishMarket.API/
RUN dotnet restore FishMarket.API/FishMarket.API.csproj
COPY . .
WORKDIR /src/FishMarket.API
RUN dotnet build FishMarket.API.csproj -c Release -o /app/build

# Publish
FROM build AS publish
RUN dotnet publish FishMarket.API.csproj -c Release -o /app/publish

# Final stage
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FishMarket.API.dll"]