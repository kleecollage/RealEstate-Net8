FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
# RUN --network=host dotnet nuget list source 
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["NetKubernetes.csproj", "NetKubernetes/"]
RUN dotnet restore "NetKubernetes/NetKubernetes.csproj"
COPY . .
WORKDIR "/src/NetKubernetes"
RUN dotnet build "./NetKubernetes.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./NetKubernetes.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NetKubernetes.dll"]
