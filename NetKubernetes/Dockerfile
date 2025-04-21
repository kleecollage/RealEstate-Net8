FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
# RUN --network=host dotnet nuget list source 
WORKDIR /app
EXPOSE 80
ENV ASPNETCORE_URLS=http://+:5000

RUN adduser -u 5678 --disabled-password --gecos "" appuser && chown -R appuser /app
USER appuser

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["NetKubernetes.csproj", "./"]
RUN dotnet restore "NetKubernetes.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "NetKubernetes.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NetKubernetes.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NetKubernetes.dll"]