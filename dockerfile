FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Rideshare.Webapi/Rideshare.Webapi.csproj", "Rideshare.Webapi/"]
COPY ["Rideshare.Application/Rideshare.Application.csproj", "Rideshare.Application/"]
COPY ["Rideshare.Domain/Rideshare.Domain.csproj", "Rideshare.Domain/"]
COPY ["Rideshare.Persistence/Rideshare.Persistence.csproj", "Rideshare.Persistence/"]
COPY ["Rideshare.Infrastructure/Rideshare.Infrastructure.csproj", "Rideshare.Infrastructure/"]
RUN dotnet restore "Rideshare.Webapi/Rideshare.Webapi.csproj"
COPY . .
WORKDIR "/src/Rideshare.Webapi"
RUN dotnet build "Rideshare.Webapi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Rideshare.Webapi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Rideshare.Webapi.dll"]