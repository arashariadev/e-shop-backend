FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src

COPY . .
RUN dotnet restore EShop.Api/EShop.Api.csproj
WORKDIR /src/EShop.Api
RUN dotnet build EShop.Api.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish "EShop.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EShop.dll"]
