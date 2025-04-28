FROM mcr.microsoft.com/dotnet/aspnet:8.0-nanoserver-1809 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0-nanoserver-1809 AS build
ARG configuration=Release
WORKDIR /src
COPY ["IDal_Repository/IDal_Repository.csproj", "IDal_Repository/"]
RUN dotnet restore "IDal_Repository\IDal_Repository.csproj"
COPY . .
WORKDIR "/src/IDal_Repository"
RUN dotnet build "IDal_Repository.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "IDal_Repository.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "IDal_Repository.dll"]
