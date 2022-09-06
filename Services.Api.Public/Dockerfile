#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["Services.Api.Public/Services.Api.Public.csproj", "Services.Api.Public/"]
COPY ["Shared.Api/Shared.Api.csproj", "Shared.Api/"]
COPY ["Shared.CustomDistibutedCache/Shared.CustomDistibutedCache.csproj", "Shared.CustomDistibutedCache/"]
COPY ["Shared.WebHost/Shared.WebHost.csproj", "Shared.WebHost/"]
COPY ["Shared.Helper/Shared.Helper.csproj", "Shared.Helper/"]
COPY ["Shared.ConfigurationHelper/Shared.ConfigurationHelper.csproj", "Shared.ConfigurationHelper/"]
COPY ["Shared.EventBus.RabbitMQ/Shared.EventBus.RabbitMQ.csproj", "Shared.EventBus.RabbitMQ/"]
COPY ["Shared.EventBus/Shared.EventBus.csproj", "Shared.EventBus/"]
COPY ["Repo.ReadWrite.Events/Repo.ReadWrite.Events.csproj", "Repo.ReadWrite.Events/"]
COPY ["Translation.DbContext/Translation.DbContext.csproj", "Translation.DbContext/"]
COPY ["Repo.ReadOnly/Repo.ReadOnly.csproj", "Repo.ReadOnly/"]
COPY ["Repo.ReadOnly.Models/Repo.ReadOnly.Models.csproj", "Repo.ReadOnly.Models/"]
COPY ["Databases.Contexts.Public.Models/Databases.Contexts.Public.Models.csproj", "Databases.Contexts.Public.Models/"]
COPY ["Shared.EsiConnector.Models/Shared.EsiConnector.Models.csproj", "Shared.EsiConnector.Models/"]
COPY ["Repo.Base/Repo.Base.csproj", "Repo.Base/"]
COPY ["Databases.Contexts.Public/Databases.Contexts.Public.csproj", "Databases.Contexts.Public/"]
COPY ["Shared.Models/Shared.Models.csproj", "Shared.Models/"]
COPY ["Shared.CompareObjects/Shared.CompareObjects.csproj", "Shared.CompareObjects/"]
COPY ["packages/", "packages/"]
RUN dotnet restore "Services.Api.Public/Services.Api.Public.csproj" --source /src/packages  --source https://api.nuget.org/v3/index.json
COPY . .
WORKDIR "/src/Services.Api.Public"
RUN dotnet build "Services.Api.Public.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Services.Api.Public.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Services.Api.Public.dll"]