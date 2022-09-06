#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["Services.Jobs.EventSubscribers/Services.Jobs.EventSubscribers.csproj", "Services.Jobs.EventSubscribers/"]
COPY ["Repo.ReadWrite.Events/Repo.ReadWrite.Events.csproj", "Repo.ReadWrite.Events/"]
COPY ["Shared.EventBus/Shared.EventBus.csproj", "Shared.EventBus/"]
COPY ["Services.Jobs.Migrations/Services.Jobs.Migrations.csproj", "Services.Jobs.Migrations/"]
COPY ["Services.Jobs.Core/Services.Jobs.Core.csproj", "Services.Jobs.Core/"]
COPY ["Databases.Contexts.Public.Models/Databases.Contexts.Public.Models.csproj", "Databases.Contexts.Public.Models/"]
COPY ["Shared.EsiConnector.Models/Shared.EsiConnector.Models.csproj", "Shared.EsiConnector.Models/"]
COPY ["Shared.Helper/Shared.Helper.csproj", "Shared.Helper/"]
COPY ["Repo.ReadWrite/Repo.ReadWrite.csproj", "Repo.ReadWrite/"]
COPY ["Repo.Base/Repo.Base.csproj", "Repo.Base/"]
COPY ["Databases.Contexts.Public/Databases.Contexts.Public.csproj", "Databases.Contexts.Public/"]
COPY ["Shared.Models/Shared.Models.csproj", "Shared.Models/"]
COPY ["Shared.CompareObjects/Shared.CompareObjects.csproj", "Shared.CompareObjects/"]
COPY ["Shared.ConfigurationHelper/Shared.ConfigurationHelper.csproj", "Shared.ConfigurationHelper/"]
COPY ["Services.EsiConnector.Jobs/Services.EsiConnector.Jobs.csproj", "Services.EsiConnector.Jobs/"]
COPY ["Shared.EsiConnector/Shared.EsiConnector.csproj", "Shared.EsiConnector/"]
COPY ["Services.EsiConnector/Services.EsiConnector.csproj", "Services.EsiConnector/"]
COPY ["Shared.WebHost/Shared.WebHost.csproj", "Shared.WebHost/"]
COPY ["Shared.EventBus.RabbitMQ/Shared.EventBus.RabbitMQ.csproj", "Shared.EventBus.RabbitMQ/"]
COPY ["packages/", "packages/"]
RUN dotnet restore "Services.Jobs.EventSubscribers/Services.Jobs.EventSubscribers.csproj"  --source /src/packages  --source https://api.nuget.org/v3/index.json
COPY . .
WORKDIR "/src/Services.Jobs.EventSubscribers"
RUN dotnet build "Services.Jobs.EventSubscribers.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Services.Jobs.EventSubscribers.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Services.Jobs.EventSubscribers.dll"]