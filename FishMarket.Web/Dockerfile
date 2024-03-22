# Base image
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

# Publish Web project
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY FishMarket.Web/FishMarket.Web.csproj FishMarket.Web/
RUN dotnet restore FishMarket.Web/FishMarket.Web.csproj
COPY . .
WORKDIR /src/FishMarket.Web
RUN dotnet build FishMarket.Web.csproj -c Release -o /app/build

# Publish
FROM build AS publish
RUN dotnet publish FishMarket.Web.csproj -c Release -o /app/publish

# Final stage
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FishMarket.Web.dll"]
