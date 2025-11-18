FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["TruyenHayPro.Web/TruyenHayPro.Web.Client/TruyenHayPro.Web.Client.csproj", "TruyenHayPro.Web/TruyenHayPro.Web.Client/"]
RUN dotnet restore "TruyenHayPro.Web/TruyenHayPro.Web.Client/TruyenHayPro.Web.Client.csproj"
COPY . .
WORKDIR "/src/TruyenHayPro.Web/TruyenHayPro.Web.Client"
RUN dotnet build "./TruyenHayPro.Web.Client.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./TruyenHayPro.Web.Client.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TruyenHayPro.Web.Client.dll"]
