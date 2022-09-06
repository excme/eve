#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["Clients.Web/Clients.Web.csproj", "Clients.Web/"]
COPY ["Shared.WebHost/Shared.WebHost.csproj", "Shared.WebHost/"]
COPY ["Shared.Helper/Shared.Helper.csproj", "Shared.Helper/"]
COPY ["Shared.ConfigurationHelper/Shared.ConfigurationHelper.csproj", "Shared.ConfigurationHelper/"]
COPY ["Shared.EventBus.RabbitMQ/Shared.EventBus.RabbitMQ.csproj", "Shared.EventBus.RabbitMQ/"]
COPY ["Shared.EventBus/Shared.EventBus.csproj", "Shared.EventBus/"]
COPY ["Repo.ReadOnly.Events/Repo.ReadOnly.Events.csproj", "Repo.ReadOnly.Events/"]
COPY ["Repo.Base/Repo.Base.csproj", "Repo.Base/"]
COPY ["Databases.Contexts.Public/Databases.Contexts.Public.csproj", "Databases.Contexts.Public/"]
COPY ["Databases.Contexts.Public.Models/Databases.Contexts.Public.Models.csproj", "Databases.Contexts.Public.Models/"]
COPY ["Shared.EsiConnector.Models/Shared.EsiConnector.Models.csproj", "Shared.EsiConnector.Models/"]
COPY ["Shared.Models/Shared.Models.csproj", "Shared.Models/"]
COPY ["Shared.CompareObjects/Shared.CompareObjects.csproj", "Shared.CompareObjects/"]
COPY ["packages/", "packages/"]
RUN dotnet restore "Clients.Web/Clients.Web.csproj" --source /src/packages  --source https://api.nuget.org/v3/index.json

COPY . .

WORKDIR "/src/Clients.Web/."

# Загрузка node_modules
RUN curl -sL https://deb.nodesource.com/setup_10.x |  bash -
RUN apt-get install -y nodejs
RUN npm i

run ls
#Компиляция typescript
#RUN npm install -g typescript 
#RUN tsc --build tsconfig.prod.json
RUN cp -f tsconfig.prod.json tsconfig.json

#Выполнение gulp
RUN npm install -g gulp
# css
RUN gulp css_prod
# js

RUN dotnet build "Clients.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Clients.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Clients.Web.dll"]