# Gebruik de Linux versie van .NET SDK
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Gebruik de Linux versie van .NET SDK
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
# Kopieer de csproj-bestanden naar de juiste directories binnen de container
COPY ["bookify/bookifyWEBApi.csproj", "bookify/"]
COPY ["DataAcces/DataAccess.csproj", "DataAcces/"]
COPY ["Interfaces/Interfaces.csproj", "Interfaces/"]
COPY ["Domain/Logic.csproj", "Domain/"]

# Herstel afhankelijkheden
RUN dotnet restore "bookify/bookifyWEBApi.csproj"

# Kopieer de overige bestanden van de bron naar de container
COPY . .

# Bouw het project
RUN dotnet build "bookify/bookifyWEBApi.csproj" -c ${BUILD_CONFIGURATION} -o /app/build

FROM build AS publish
RUN dotnet publish "bookify/bookifyWEBApi.csproj" -c ${BUILD_CONFIGURATION} -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
RUN adduser --disabled-password --gecos '' appuser
USER appuser
ENTRYPOINT ["dotnet", "bookifyWEBApi.dll"]
