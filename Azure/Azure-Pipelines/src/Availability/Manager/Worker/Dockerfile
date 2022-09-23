FROM mcr.microsoft.com/dotnet/runtime:5.0-alpine AS base
RUN apk add --no-cache tzdata \
    && cp /usr/share/zoneinfo/Brazil/East /etc/localtime
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0-alpine AS build
WORKDIR /src
COPY ["nuget.config", "."]
COPY ["src/Availability/Manager/Worker/Availability.Manager.Worker.csproj", "src/Availability/Manager/Worker/"]
COPY ["src/Shared/Messaging/Configuration/Shared.Messaging.Configuration.csproj", "src/Shared/Messaging/Configuration/"]
COPY ["src/Shared/Worker/Shared.Worker.csproj", "src/Shared/Worker/"]
COPY ["src/Shared/Job/Configuration/Shared.Job.Configuration.csproj", "src/Shared/Job/Configuration/"]
COPY ["src/Shared/Messaging/Contracts/Shared.Messaging.Contracts.csproj", "src/Shared/Messaging/Contracts/"]
COPY ["src/Shared/Backend/Shared.Backend.csproj", "src/Shared/Backend/"]
COPY ["src/Shared/Persistence/Mongo/Shared.Persistence.Mongo.csproj", "src/Shared/Persistence/Mongo/"]
RUN dotnet restore "src/Availability/Manager/Worker/Availability.Manager.Worker.csproj"
COPY . .
WORKDIR "/src/src/Availability/Manager/Worker"
RUN dotnet build "Availability.Manager.Worker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Availability.Manager.Worker.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Catalog.Integration.Availability.Manager.Worker.dll"]
