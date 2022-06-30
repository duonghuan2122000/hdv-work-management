#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["notifyservice/HDV.Nhom2.NotifyService/HDV.Nhom2.NotifyService/HDV.Nhom2.NotifyService.HttpApi/HDV.Nhom2.NotifyService.HttpApi.csproj", "notifyservice/HDV.Nhom2.NotifyService/HDV.Nhom2.NotifyService/HDV.Nhom2.NotifyService.HttpApi/"]
RUN dotnet restore "notifyservice/HDV.Nhom2.NotifyService/HDV.Nhom2.NotifyService/HDV.Nhom2.NotifyService.HttpApi/HDV.Nhom2.NotifyService.HttpApi.csproj"
COPY . .
WORKDIR "/src/notifyservice/HDV.Nhom2.NotifyService/HDV.Nhom2.NotifyService/HDV.Nhom2.NotifyService.HttpApi"
RUN dotnet build "HDV.Nhom2.NotifyService.HttpApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HDV.Nhom2.NotifyService.HttpApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HDV.Nhom2.NotifyService.HttpApi.dll"]
