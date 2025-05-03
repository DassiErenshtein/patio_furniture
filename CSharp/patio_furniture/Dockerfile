# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["patio_furniture/patio_furniture.csproj", "patio_furniture/"]
COPY ["Bll_Services/Bll_Services.csproj", "Bll_Services/"]
COPY ["IBll_Services/IBll_Services.csproj", "IBll_Services/"]
COPY ["Dto_Command/Dto_Command.csproj", "Dto_Command/"]
COPY ["IDal_Repository/IDal_Repository.csproj", "IDal_Repository/"]
COPY ["Dal_Repository/Dal_Repository.csproj", "Dal_Repository/"]
RUN dotnet restore "./patio_furniture/patio_furniture.csproj"
COPY . .
WORKDIR "/src/patio_furniture"
RUN dotnet build "./patio_furniture.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./patio_furniture.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "patio_furniture.dll"]