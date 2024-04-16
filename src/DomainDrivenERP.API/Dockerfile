#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Directory.Build.props", "."]
COPY ["DomainDrivenERP.API/DomainDrivenERP.API.csproj", "DomainDrivenERP.API/"]
COPY ["DomainDrivenERP.Infrastructure/DomainDrivenERP.Infrastructure.csproj", "DomainDrivenERP.Infrastructure/"]
COPY ["DomainDrivenERP.Application/DomainDrivenERP.Application.csproj", "DomainDrivenERP.Application/"]
COPY ["DomainDrivenERP.Domain/DomainDrivenERP.Domain.csproj", "DomainDrivenERP.Domain/"]
COPY ["DomainDrivenERP.Persistence/DomainDrivenERP.Persistence.csproj", "DomainDrivenERP.Persistence/"]
COPY ["DomainDrivenERP.Presentation/DomainDrivenERP.Presentation.csproj", "DomainDrivenERP.Presentation/"]
RUN dotnet restore "./DomainDrivenERP.API/DomainDrivenERP.API.csproj"
COPY . .
WORKDIR "/src/DomainDrivenERP.API"
RUN dotnet build "./DomainDrivenERP.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./DomainDrivenERP.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DomainDrivenERP.API.dll"]