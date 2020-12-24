FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["Warehouse.WebApi/Warehouse.WebApi.csproj", "Warehouse.WebApi/"]
COPY ["Warehouse.LegacyData/Warehouse.LegacyData.csproj", "Warehouse.LegacyData/"]
COPY ["Warehouse.Repositories/Warehouse.Repositories.csproj", "Warehouse.Repositories/"]
COPY ["Warehouse.Domain/Warehouse.Domain.csproj", "Warehouse.Domain/"]
COPY ["Warehouse.Data/Warehouse.Data.csproj", "Warehouse.Data/"]
COPY ["Warehouse.Protocol/Warehouse.Protocol.csproj", "Warehouse.Protocol/"]
RUN dotnet restore -s "https://api.nuget.org/v3/index.json" "Warehouse.WebApi/Warehouse.WebApi.csproj"
COPY . .
WORKDIR "/src/Warehouse.WebApi"
RUN dotnet build "Warehouse.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Warehouse.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

RUN ln -sf /usr/share/zoneinfo/Europe/Moscow /etc/localtime
ENTRYPOINT dotnet Warehouse.WebApi.dll --environment=$dotnet
