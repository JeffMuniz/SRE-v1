FROM mcr.microsoft.com/dotnet/runtime:5.0-alpine AS base
RUN apk add --no-cache tzdata \
    && cp /usr/share/zoneinfo/Brazil/East /etc/localtime
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0-alpine AS build
WORKDIR /src
COPY ["nuget.config", "."]
COPY ["src/Product/Supplier/Magalu/Worker/Product.Supplier.Magalu.Worker.csproj", "src/Product/Supplier/Magalu/Worker/"]
COPY ["src/Shared/Messaging/Configuration/Shared.Messaging.Configuration.csproj", "src/Shared/Messaging/Configuration/"]
COPY ["src/Product/Supplier/Shared/Worker/Product.Supplier.Shared.Worker.csproj", "src/Product/Supplier/Shared/Worker/"]
COPY ["src/Shared/Backend/Shared.Backend.csproj", "src/Shared/Backend/"]
COPY ["src/Shared/Messaging/Contracts/Shared.Messaging.Contracts.csproj", "src/Shared/Messaging/Contracts/"]
COPY ["src/Shared/Worker/Shared.Worker.csproj", "src/Shared/Worker/"]
COPY ["src/Shared/Job/Configuration/Shared.Job.Configuration.csproj", "src/Shared/Job/Configuration/"]
COPY ["src/Shared/Persistence/AzureBlob/Shared.Persistence.AzureBlob.csproj", "src/Shared/Persistence/AzureBlob/"]
COPY ["src/Shared/Persistence/Mongo/Shared.Persistence.Mongo.csproj", "src/Shared/Persistence/Mongo/"]
COPY ["src/Shared/Keywords/Shared.Keywords.csproj", "src/Shared/Keywords/"]
COPY ["src/Shared/Job/Abstractions/Shared.Job.Abstractions.csproj", "src/Shared/Job/Abstractions/"]
RUN dotnet restore "src/Product/Supplier/Magalu/Worker/Product.Supplier.Magalu.Worker.csproj"
COPY . .
WORKDIR "/src/src/Product/Supplier/Magalu/Worker"
RUN dotnet build "Product.Supplier.Magalu.Worker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Product.Supplier.Magalu.Worker.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Catalog.Integration.Product.Supplier.Magalu.Worker.dll"]
