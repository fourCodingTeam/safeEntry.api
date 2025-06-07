# build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

#copy
COPY SafeEntry.sln ./
COPY src/SafeEntry.Api/*.csproj ./src/SafeEntry.Api/
COPY src/SafeEntry.Application/*.csproj ./src/SafeEntry.Application/
COPY src/SafeEntry.Contracts/*.csproj ./src/SafeEntry.Contracts/
COPY src/SafeEntry.Domain/*.csproj ./src/SafeEntry.Domain/
COPY src/SafeEntry.Infrastructure/*.csproj ./src/SafeEntry.Infrastructure/

RUN dotnet restore

COPY . .

WORKDIR /app/src/SafeEntry.Api
RUN dotnet publish -c Release -o /out

# runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /out .

ENV ASPNETCORE_URLS=http://+:10000
EXPOSE 10000

ENTRYPOINT ["dotnet", "SafeEntry.Api.dll"]
