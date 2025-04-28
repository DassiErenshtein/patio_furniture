FROM mcr.microsoft.com/dotnet/aspnet:8.0-nanoserver-1809 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0-nanoserver-1809 AS build
ARG configuration=Release
WORKDIR /src
COPY ["Dal_Repository/Dal_Repository.csproj", "Dal_Repository/"]
RUN dotnet restore "Dal_Repository\Dal_Repository.csproj"
COPY . .
WORKDIR "/src/Dal_Repository"
RUN dotnet build "Dal_Repository.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "Dal_Repository.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Dal_Repository.dll"]
