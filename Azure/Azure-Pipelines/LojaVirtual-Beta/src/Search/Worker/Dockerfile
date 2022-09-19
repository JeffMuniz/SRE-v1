FROM mcr.microsoft.com/dotnet/runtime:5.0-alpine AS base
RUN apk add --no-cache tzdata \
    && cp /usr/share/zoneinfo/Brazil/East /etc/localtime
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0-alpine AS build
WORKDIR /src
COPY ["nuget.config", "."]
COPY ["src/Search/Worker/Search.Worker.csproj", "src/Search/Worker/"]
COPY ["src/Shared/Messaging/Configuration/Shared.Messaging.Configuration.csproj", "src/Shared/Messaging/Configuration/"]
COPY ["src/Shared/Messaging/Contracts/Shared.Messaging.Contracts.csproj", "src/Shared/Messaging/Contracts/"]
COPY ["src/Shared/Backend/Shared.Backend.csproj", "src/Shared/Backend/"]
COPY ["src/Shared/Worker/Shared.Worker.csproj", "src/Shared/Worker/"]
COPY ["src/Shared/Job/Configuration/Shared.Job.Configuration.csproj", "src/Shared/Job/Configuration/"]
RUN dotnet restore "src/Search/Worker/Search.Worker.csproj"
COPY . .
WORKDIR "/src/src/Search/Worker"
RUN dotnet build "Search.Worker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Search.Worker.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Catalog.Integration.Search.Worker.dll"]
