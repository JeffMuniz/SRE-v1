FROM mcr.microsoft.com/dotnet/aspnet:5.0-alpine AS base
RUN apk add --no-cache tzdata \
    && cp /usr/share/zoneinfo/Brazil/East /etc/localtime
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-alpine AS build
WORKDIR /src
COPY ["nuget.config", "."]
COPY ["src/Product/Enrichment/Macnaima/Api/Product.Enrichment.Macnaima.Api.csproj", "src/Product/Enrichment/Macnaima/Api/"]
COPY ["src/Shared/Backend/Shared.Backend.csproj", "src/Shared/Backend/"]
COPY ["src/Shared/Messaging/Contracts/Shared.Messaging.Contracts.csproj", "src/Shared/Messaging/Contracts/"]
COPY ["src/Shared/Messaging/Configuration/Shared.Messaging.Configuration.csproj", "src/Shared/Messaging/Configuration/"]
RUN dotnet restore "src/Product/Enrichment/Macnaima/Api/Product.Enrichment.Macnaima.Api.csproj"
COPY . .
WORKDIR "/src/src/Product/Enrichment/Macnaima/Api"
RUN dotnet build "Product.Enrichment.Macnaima.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Product.Enrichment.Macnaima.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Catalog.Integration.Product.Enrichment.Macnaima.Api.dll"]
