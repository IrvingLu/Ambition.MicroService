#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["NMS.File.Web/NMS.File.Web.csproj", "NMS.File.Web/"]
COPY ["Shared/Shared.Infrastructure.Core/Shared.Infrastructure.Core.csproj", "Shared/Shared.Infrastructure.Core/"]
COPY ["Shared/Shared.Doamin.Abstractions/Shared.Domain.Abstractions.csproj", "Shared/Shared.Doamin.Abstractions/"]
RUN dotnet restore "NMS.File.Web/NMS.File.Web.csproj"
COPY . .
WORKDIR "/src/NMS.File.Web"
RUN dotnet build "NMS.File.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NMS.File.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NMS.File.Web.dll"]
