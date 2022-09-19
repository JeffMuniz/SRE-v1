FROM mcr.microsoft.com/dotnet/runtime:5.0-alpine AS base
RUN apk add --no-cache tzdata \
    && cp /usr/share/zoneinfo/Brazil/East /etc/localtime
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0-alpine AS build
WORKDIR /src
COPY ["nuget.config", "."]
COPY ["src/Price/History/Worker/Price.History.Worker.csproj", "src/Price/History/Worker/"]
COPY ["src/Shared/Worker/Shared.Worker.csproj", "src/Shared/Worker/"]
RUN dotnet restore "src/Price/History/Worker/Price.History.Worker.csproj"
COPY . .
WORKDIR "/src/src/Price/History/Worker"
RUN dotnet build "Price.History.Worker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Price.History.Worker.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Catalog.Integration.Price.History.Worker.dll"]
