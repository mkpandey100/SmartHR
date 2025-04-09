#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8091

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/ApiGateway/SmartHR.ApiGateway/SmartHR.ApiGateway.csproj", "src/ApiGateway/SmartHR.ApiGateway/"]
RUN dotnet restore "./src/ApiGateway/SmartHR.ApiGateway/./SmartHR.ApiGateway.csproj"
COPY . .
WORKDIR "/src/src/ApiGateway/SmartHR.ApiGateway"
RUN dotnet build "./SmartHR.ApiGateway.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./SmartHR.ApiGateway.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENV ASPNETCORE_URLS=http://+:8091
ENTRYPOINT ["dotnet", "SmartHR.ApiGateway.dll"]