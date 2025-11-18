FROM mcr.microsoft.com/dotnet/runtime:10.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["TruyenHayPro.Background/TruyenHayPro.Background.csproj", "TruyenHayPro.Background/"]
RUN dotnet restore "TruyenHayPro.Background/TruyenHayPro.Background.csproj"
COPY . .
WORKDIR "/src/TruyenHayPro.Background"
RUN dotnet build "./TruyenHayPro.Background.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./TruyenHayPro.Background.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TruyenHayPro.Background.dll"]
