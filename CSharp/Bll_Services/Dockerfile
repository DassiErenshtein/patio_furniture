FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

USER app
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["Bll_Services/Bll_Services.csproj", "Bll_Services/"]
RUN dotnet restore "Bll_Services/Bll_Services.csproj"
COPY . .
WORKDIR "/src/Bll_Services"
RUN dotnet build "Bll_Services.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "Bll_Services.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Bll_Services.dll"]
