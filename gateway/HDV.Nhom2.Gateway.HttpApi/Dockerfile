#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["gateway/HDV.Nhom2.Gateway.HttpApi/HDV.Nhom2.Gateway.HttpApi.csproj", "gateway/HDV.Nhom2.Gateway.HttpApi/"]
COPY ["HDV.Nhom2.Infrastructure/HDV.Nhom2.Infrastructure.csproj", "HDV.Nhom2.Infrastructure/"]
COPY ["core/HDV.Nhom2.Infrastructure.Contracts/HDV.Nhom2.Infrastructure.Contracts.csproj", "core/HDV.Nhom2.Infrastructure.Contracts/"]
COPY ["gateway/HDV.Nhom2.Gateway.BL/HDV.Nhom2.Gateway.BL.csproj", "gateway/HDV.Nhom2.Gateway.BL/"]
RUN dotnet restore "gateway/HDV.Nhom2.Gateway.HttpApi/HDV.Nhom2.Gateway.HttpApi.csproj"
COPY . .
WORKDIR "/src/gateway/HDV.Nhom2.Gateway.HttpApi"
RUN dotnet build "HDV.Nhom2.Gateway.HttpApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HDV.Nhom2.Gateway.HttpApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HDV.Nhom2.Gateway.HttpApi.dll"]
