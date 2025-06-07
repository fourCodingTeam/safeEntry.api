# Etapa 1: build
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app

# Copia os arquivos da solução
COPY SafeEntry.sln ./
COPY src/SafeEntry.API/*.csproj ./src/SafeEntry.API/
COPY src/SafeEntry.Application/*.csproj ./src/SafeEntry.Application/
COPY src/SafeEntry.Domain/*.csproj ./src/SafeEntry.Domain/
COPY src/SafeEntry.Infrastructure/*.csproj ./src/SafeEntry.Infrastructure/

# Restaura os pacotes
RUN dotnet restore

# Copia o restante do código
COPY . .

# Publica o projeto principal
WORKDIR /app/src/SafeEntry.API
RUN dotnet publish -c Release -o /out

# Etapa 2: runtime
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build /out .

# Configura porta
ENV ASPNETCORE_URLS=http://+:10000
EXPOSE 10000

ENTRYPOINT ["dotnet", "SafeEntry.API.dll"]
