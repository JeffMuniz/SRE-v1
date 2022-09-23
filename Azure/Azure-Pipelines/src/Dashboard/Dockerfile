FROM mcr.microsoft.com/dotnet/aspnet:5.0-alpine AS base
RUN apk add --no-cache tzdata \
    && cp /usr/share/zoneinfo/Brazil/East /etc/localtime
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-alpine AS build
WORKDIR /src
COPY ["nuget.config", "."]
COPY ["src/Dashboard/Dashboard.Web.csproj", "src/Dashboard/"]
COPY ["src/Shared/Job/Abstractions/Shared.Job.Abstractions.csproj", "src/Shared/Job/Abstractions/"]
COPY ["src/Shared/Job/Configuration/Shared.Job.Configuration.csproj", "src/Shared/Job/Configuration/"]
RUN dotnet restore "src/Dashboard/Dashboard.Web.csproj"
COPY . .
WORKDIR "/src/src/Dashboard"
RUN dotnet build "Dashboard.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Dashboard.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Catalog.Integration.Dashboard.Web.dll"]
