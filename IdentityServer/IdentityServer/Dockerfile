#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["IdentityServer/IdentityServer/IdentityServer.csproj", "IdentityServer/IdentityServer/"]
COPY ["BuildingBlocks/EventBus.Messages/EventBus.Messages.csproj", "BuildingBlocks/EventBus.Messages/"]
COPY ["IdentityServer/IdentityServer.Application/IdentityServer.Application.csproj", "IdentityServer/IdentityServer.Application/"]
COPY ["IdentityServer/IdentityServer.Domain/IdentityServer.Domain.csproj", "IdentityServer/IdentityServer.Domain/"]
COPY ["IdentityServer/IdentityServer.Infrastructure/IdentityServer.Infrastructure.csproj", "IdentityServer/IdentityServer.Infrastructure/"]
RUN dotnet restore "./IdentityServer/IdentityServer/./IdentityServer.csproj"
COPY . .
WORKDIR "/src/IdentityServer/IdentityServer"
RUN dotnet build "./IdentityServer.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./IdentityServer.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "IdentityServer.dll"]