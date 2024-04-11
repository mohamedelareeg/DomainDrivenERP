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
COPY ["CleanArchitectureWithDDD.API/CleanArchitectureWithDDD.API.csproj", "CleanArchitectureWithDDD.API/"]
COPY ["CleanArchitectureWithDDD.Infrastructure/CleanArchitectureWithDDD.Infrastructure.csproj", "CleanArchitectureWithDDD.Infrastructure/"]
COPY ["CleanArchitectureWithDDD.Application/CleanArchitectureWithDDD.Application.csproj", "CleanArchitectureWithDDD.Application/"]
COPY ["CleanArchitectureWithDDD.Domain/CleanArchitectureWithDDD.Domain.csproj", "CleanArchitectureWithDDD.Domain/"]
COPY ["CleanArchitectureWithDDD.Persistence/CleanArchitectureWithDDD.Persistence.csproj", "CleanArchitectureWithDDD.Persistence/"]
COPY ["CleanArchitectureWithDDD.Presentation/CleanArchitectureWithDDD.Presentation.csproj", "CleanArchitectureWithDDD.Presentation/"]
RUN dotnet restore "./CleanArchitectureWithDDD.API/CleanArchitectureWithDDD.API.csproj"
COPY . .
WORKDIR "/src/CleanArchitectureWithDDD.API"
RUN dotnet build "./CleanArchitectureWithDDD.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./CleanArchitectureWithDDD.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CleanArchitectureWithDDD.API.dll"]