#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8094

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Services/PayrollService/SmartHR.PayrollService.API/SmartHR.PayrollService.API.csproj", "src/Services/PayrollService/SmartHR.PayrollService.API/"]
RUN dotnet restore "./src/Services/PayrollService/SmartHR.PayrollService.API/./SmartHR.PayrollService.API.csproj"
COPY . .
WORKDIR "/src/src/Services/PayrollService/SmartHR.PayrollService.API"
RUN dotnet build "./SmartHR.PayrollService.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./SmartHR.PayrollService.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_URLS=http://+:8094
ENTRYPOINT ["dotnet", "SmartHR.PayrollService.API.dll"]