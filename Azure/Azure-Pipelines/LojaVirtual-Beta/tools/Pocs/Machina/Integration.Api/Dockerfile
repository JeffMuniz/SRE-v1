#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-alpine AS base
RUN apk add --no-cache tzdata \
    && cp /usr/share/zoneinfo/Brazil/East /etc/localtime
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-alpine AS build
WORKDIR /src
COPY ["nuget.config", "."]
COPY ["tools/Pocs/macnaima/Integration.Api/Integration.Api.csproj", "tools/Pocs/macnaima/Integration.Api/"]
RUN dotnet restore "tools/Pocs/macnaima/Integration.Api/Integration.Api.csproj"
COPY . .
WORKDIR "/src/tools/Pocs/macnaima/Integration.Api"
RUN dotnet build "Integration.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Integration.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Integration.Api.dll"]
