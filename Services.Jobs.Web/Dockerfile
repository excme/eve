#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["Services.Jobs.Web/Services.Jobs.Web.csproj", "Services.Jobs.Web/"]
COPY ["Services.Warface.Jobs/Services.Warface.Jobs.csproj", "Services.Warface.Jobs/"]
COPY ["Services.Jobs.Core/Services.Jobs.Core.csproj", "Services.Jobs.Core/"]
COPY ["Databases.Contexts.Public.Models/Databases.Contexts.Public.Models.csproj", "Databases.Contexts.Public.Models/"]
COPY ["Shared.EsiConnector.Models/Shared.EsiConnector.Models.csproj", "Shared.EsiConnector.Models/"]
COPY ["Shared.Helper/Shared.Helper.csproj", "Shared.Helper/"]
COPY ["Repo.ReadWrite/Repo.ReadWrite.csproj", "Repo.ReadWrite/"]
COPY ["Repo.ReadWrite.Events/Repo.ReadWrite.Events.csproj", "Repo.ReadWrite.Events/"]
COPY ["Shared.EventBus/Shared.EventBus.csproj", "Shared.EventBus/"]
COPY ["Repo.Base/Repo.Base.csproj", "Repo.Base/"]
COPY ["Databases.Contexts.Public/Databases.Contexts.Public.csproj", "Databases.Contexts.Public/"]
COPY ["Shared.Models/Shared.Models.csproj", "Shared.Models/"]
COPY ["Shared.CompareObjects/Shared.CompareObjects.csproj", "Shared.CompareObjects/"]
COPY ["Shared.ConfigurationHelper/Shared.ConfigurationHelper.csproj", "Shared.ConfigurationHelper/"]
COPY ["Shared.Hangfire.Redis.StackExchange/Shared.Hangfire.Redis.StackExchange.csproj", "Shared.Hangfire.Redis.StackExchange/"]
COPY ["Shared.WebHost/Shared.WebHost.csproj", "Shared.WebHost/"]
COPY ["Shared.EventBus.RabbitMQ/Shared.EventBus.RabbitMQ.csproj", "Shared.EventBus.RabbitMQ/"]
COPY ["Services.Jobs.Processings/Services.Jobs.Processings.csproj", "Services.Jobs.Processings/"]
COPY ["Services.Jobs.Market/Services.Jobs.Market.csproj", "Services.Jobs.Market/"]
COPY ["Services.EsiConnector.Jobs/Services.EsiConnector.Jobs.csproj", "Services.EsiConnector.Jobs/"]
COPY ["Shared.EsiConnector/Shared.EsiConnector.csproj", "Shared.EsiConnector/"]
COPY ["Services.EsiConnector/Services.EsiConnector.csproj", "Services.EsiConnector/"]
COPY ["Shared.Hangfire.Custom/Shared.Hangfire.Custom.csproj", "Shared.Hangfire.Custom/"]
COPY ["packages/", "packages/"]
RUN dotnet restore "Services.Jobs.Web/Services.Jobs.Web.csproj"  --source /src/packages  --source https://api.nuget.org/v3/index.json

COPY . .
WORKDIR "/src/Services.Jobs.Web"
RUN dotnet build "Services.Jobs.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Services.Jobs.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Services.Jobs.Web.dll"]