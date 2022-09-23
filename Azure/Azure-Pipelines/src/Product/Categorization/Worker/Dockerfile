FROM mcr.microsoft.com/dotnet/runtime:5.0-alpine AS base
RUN apk add --no-cache tzdata \
    && cp /usr/share/zoneinfo/Brazil/East /etc/localtime
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0-alpine AS build
WORKDIR /src
COPY ["nuget.config", "."]
COPY ["src/Product/Categorization/Worker/Product.Categorization.Worker.csproj", "src/Product/Categorization/Worker/"]
COPY ["src/Shared/Backend/Shared.Backend.csproj", "src/Shared/Backend/"]
COPY ["src/Shared/Messaging/Configuration/Shared.Messaging.Configuration.csproj", "src/Shared/Messaging/Configuration/"]
COPY ["src/Shared/Messaging/Contracts/Shared.Messaging.Contracts.csproj", "src/Shared/Messaging/Contracts/"]
COPY ["src/Shared/Persistence/Mongo/Shared.Persistence.Mongo.csproj", "src/Shared/Persistence/Mongo/"]
COPY ["src/Shared/Worker/Shared.Worker.csproj", "src/Shared/Worker/"]
RUN dotnet restore "src/Product/Categorization/Worker/Product.Categorization.Worker.csproj"
COPY . .
WORKDIR "/src/src/Product/Categorization/Worker"
RUN dotnet build "Product.Categorization.Worker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Product.Categorization.Worker.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Catalog.Integration.Product.Categorization.Worker.dll"]
