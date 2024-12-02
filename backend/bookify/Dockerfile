# Gebruik de Linux versie van .NET SDK
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Gebruik de Linux versie van .NET SDK
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["backend/bookify/bookifyWEBApi.csproj", "backend/bookify/"]
COPY ["backend/DataAcces/DataAccess.csproj", "backend/DataAcces/"]
COPY ["backend/Interfaces/Interfaces.csproj", "backend/Interfaces/"]
COPY ["backend/Domain/Logic.csproj", "backend/Domain/"]
RUN dotnet restore "./backend/bookify/bookifyWEBApi.csproj"
COPY . .
WORKDIR "/src/backend/bookify"
RUN dotnet build "./bookifyWEBApi.csproj" -c %BUILD_CONFIGURATION% -o /app/build

FROM build AS publish
RUN dotnet publish "./bookifyWEBApi.csproj" -c %BUILD_CONFIGURATION% -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
RUN adduser --disabled-password --gecos '' appuser
USER appuser
ENTRYPOINT ["dotnet", "bookifyWEBApi.dll"]
