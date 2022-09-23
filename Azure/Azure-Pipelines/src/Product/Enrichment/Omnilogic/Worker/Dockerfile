FROM mcr.microsoft.com/dotnet/runtime:5.0-alpine AS base
RUN apk add --no-cache tzdata \
    && cp /usr/share/zoneinfo/Brazil/East /etc/localtime
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0-alpine AS build
WORKDIR /src
COPY ["nuget.config", "."]
COPY ["src/Product/Enrichment/Macnaima/Worker/Product.Enrichment.Macnaima.Worker.csproj", "src/Product/Enrichment/Macnaima/Worker/"]
COPY ["src/Shared/Backend/Shared.Backend.csproj", "src/Shared/Backend/"]
COPY ["src/Shared/Messaging/Contracts/Shared.Messaging.Contracts.csproj", "src/Shared/Messaging/Contracts/"]
COPY ["src/Shared/Messaging/Configuration/Shared.Messaging.Configuration.csproj", "src/Shared/Messaging/Configuration/"]
COPY ["src/Shared/Worker/Shared.Worker.csproj", "src/Shared/Worker/"]
COPY ["src/Shared/Job/Configuration/Shared.Job.Configuration.csproj", "src/Shared/Job/Configuration/"]
RUN dotnet restore "src/Product/Enrichment/Macnaima/Worker/Product.Enrichment.Macnaima.Worker.csproj"
COPY . .
WORKDIR "/src/src/Product/Enrichment/Macnaima/Worker"
RUN dotnet build "Product.Enrichment.Macnaima.Worker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Product.Enrichment.Macnaima.Worker.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Catalog.Integration.Product.Enrichment.Macnaima.Worker.dll"]
