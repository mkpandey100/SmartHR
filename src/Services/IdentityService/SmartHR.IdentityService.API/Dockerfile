#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8093

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Services/IdentityService/SmartHR.IdentityService.API/SmartHR.IdentityService.API.csproj", "src/Services/IdentityService/SmartHR.IdentityService.API/"]
RUN dotnet restore "./src/Services/IdentityService/SmartHR.IdentityService.API/./SmartHR.IdentityService.API.csproj"
COPY . .
WORKDIR "/src/src/Services/IdentityService/SmartHR.IdentityService.API"
RUN dotnet build "./SmartHR.IdentityService.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./SmartHR.IdentityService.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_URLS=http://+:8093
ENTRYPOINT ["dotnet", "SmartHR.IdentityService.API.dll"]