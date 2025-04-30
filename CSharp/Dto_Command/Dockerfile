FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

USER app
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["Dto_Command/Dto_Command.csproj", "Dto_Command/"]
RUN dotnet restore "Dto_Command/Dto_Command.csproj"
COPY . .
WORKDIR "/src/Dto_Command"
RUN dotnet build "Dto_Command.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "Dto_Command.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Dto_Command.dll"]
