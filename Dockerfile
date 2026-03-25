FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY MvpShop/MvpShop.csproj MvpShop/
RUN dotnet restore MvpShop/MvpShop.csproj

COPY MvpShop/. MvpShop/
WORKDIR /src/MvpShop
RUN dotnet publish MvpShop.csproj -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Development

COPY --from=build /app/publish .

EXPOSE 8080

ENTRYPOINT ["dotnet", "MvpShop.dll"]
