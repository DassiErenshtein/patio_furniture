FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

USER app
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["patio_furniture/patio_furniture.csproj", "patio_furniture/"]
RUN dotnet restore "patio_furniture/patio_furniture.csproj"
COPY . .
WORKDIR "/src/patio_furniture"
RUN dotnet build "patio_furniture.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "patio_furniture.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "patio_furniture.dll"]
