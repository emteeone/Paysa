#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/UGB.Paysa.Web.Mvc/UGB.Paysa.Web.Mvc.csproj", "src/UGB.Paysa.Web.Mvc/"]
COPY ["src/UGB.Paysa.Web.Core/UGB.Paysa.Web.Core.csproj", "src/UGB.Paysa.Web.Core/"]
COPY ["src/UGB.Paysa.Application/UGB.Paysa.Application.csproj", "src/UGB.Paysa.Application/"]
COPY ["src/UGB.Paysa.Application.Shared/UGB.Paysa.Application.Shared.csproj", "src/UGB.Paysa.Application.Shared/"]
COPY ["src/UGB.Paysa.Core.Shared/UGB.Paysa.Core.Shared.csproj", "src/UGB.Paysa.Core.Shared/"]
COPY ["src/UGB.Paysa.Core/UGB.Paysa.Core.csproj", "src/UGB.Paysa.Core/"]
COPY ["src/UGB.Paysa.EntityFrameworkCore/UGB.Paysa.EntityFrameworkCore.csproj", "src/UGB.Paysa.EntityFrameworkCore/"]
COPY ["src/UGB.Paysa.GraphQL/UGB.Paysa.GraphQL.csproj", "src/UGB.Paysa.GraphQL/"]
RUN dotnet restore "src/UGB.Paysa.Web.Mvc/UGB.Paysa.Web.Mvc.csproj"
COPY . .
WORKDIR "/src/src/UGB.Paysa.Web.Mvc"
RUN dotnet build "UGB.Paysa.Web.Mvc.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "UGB.Paysa.Web.Mvc.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UGB.Paysa.Web.Mvc.dll"]
