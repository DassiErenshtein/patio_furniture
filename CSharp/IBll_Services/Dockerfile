FROM mcr.microsoft.com/dotnet/aspnet:8.0-nanoserver-1809 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0-nanoserver-1809 AS build
ARG configuration=Release
WORKDIR /src
COPY ["IBll_Services/IBll_Services.csproj", "IBll_Services/"]
RUN dotnet restore "IBll_Services\IBll_Services.csproj"
COPY . .
WORKDIR "/src/IBll_Services"
RUN dotnet build "IBll_Services.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "IBll_Services.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "IBll_Services.dll"]
