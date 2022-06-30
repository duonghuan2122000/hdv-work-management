#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.
FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
#FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["notifyservice/HDV.Nhom2.EmailWorker/HDV.Nhom2.EmailWorker.csproj", "notifyservice/HDV.Nhom2.EmailWorker/"]
COPY ["HDV.Nhom2.Infrastructure/HDV.Nhom2.Infrastructure.csproj", "HDV.Nhom2.Infrastructure/"]
COPY ["core/HDV.Nhom2.Infrastructure.Contracts/HDV.Nhom2.Infrastructure.Contracts.csproj", "core/HDV.Nhom2.Infrastructure.Contracts/"]
RUN dotnet restore "notifyservice/HDV.Nhom2.EmailWorker/HDV.Nhom2.EmailWorker.csproj"
COPY . .
WORKDIR "/src/notifyservice/HDV.Nhom2.EmailWorker"
RUN dotnet build "HDV.Nhom2.EmailWorker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HDV.Nhom2.EmailWorker.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HDV.Nhom2.EmailWorker.dll"]