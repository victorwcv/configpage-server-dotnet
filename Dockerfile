FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY . .

RUN dotnet restore backend-dotnet.csproj
RUN dotnet build backend-dotnet.csproj -c Release --no-restore
RUN dotnet publish backend-dotnet.csproj -c Release -o /app/publish --no-build

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "backend-dotnet.dll"]  # Asegúrate de que el nombre sea correcto
