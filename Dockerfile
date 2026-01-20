FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY . .
RUN dotnet restore FiapCloudGames.Payments/FiapCloudGames.Payments.Api/FiapCloudGames.Payments.Api.csproj
RUN dotnet publish FiapCloudGames.Payments/FiapCloudGames.Payments.Api/FiapCloudGames.Payments.Api.csproj \
    -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

ENV ASPNETCORE_URLS=http://+:80
EXPOSE 80

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "FiapCloudGames.Payments.Api.dll"]
