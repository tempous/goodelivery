FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Goodelivery.App/Goodelivery.App.csproj", "Goodelivery.App/"]
RUN dotnet restore "Goodelivery.App/Goodelivery.App.csproj"
COPY . .
WORKDIR "/src/Goodelivery.App"
RUN dotnet build "Goodelivery.App.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Goodelivery.App.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Goodelivery.App.dll"]
