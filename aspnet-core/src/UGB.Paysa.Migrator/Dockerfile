#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/UGB.Paysa.Migrator/UGB.Paysa.Migrator.csproj", "src/UGB.Paysa.Migrator/"]
COPY ["src/UGB.Paysa.EntityFrameworkCore/UGB.Paysa.EntityFrameworkCore.csproj", "src/UGB.Paysa.EntityFrameworkCore/"]
COPY ["src/UGB.Paysa.Core/UGB.Paysa.Core.csproj", "src/UGB.Paysa.Core/"]
COPY ["src/UGB.Paysa.Core.Shared/UGB.Paysa.Core.Shared.csproj", "src/UGB.Paysa.Core.Shared/"]
RUN dotnet restore "src/UGB.Paysa.Migrator/UGB.Paysa.Migrator.csproj"
COPY . .
WORKDIR "/src/src/UGB.Paysa.Migrator"
RUN dotnet build "UGB.Paysa.Migrator.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "UGB.Paysa.Migrator.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UGB.Paysa.Migrator.dll", "-s"]
