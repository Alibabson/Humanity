# Etap 1: budowanie projektu
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Skopiuj plik projektu i przywróć zależności
COPY Humanity.csproj .
RUN dotnet restore Humanity.csproj

# Skopiuj resztę plików i zbuduj aplikację
COPY . .
RUN dotnet build Humanity.csproj -c Release -o /app/build

# Etap 2: uruchamianie aplikacji (wystarczy SDK, bo to konsola)
FROM mcr.microsoft.com/dotnet/sdk:8.0
WORKDIR /app
COPY --from=build /app/build .
ENTRYPOINT ["dotnet", "Humanity.dll"]
