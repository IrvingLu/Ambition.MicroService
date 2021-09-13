#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Services/Identity/NMS.Identity.Web.csproj", "Services/Identity/"]
COPY ["Shared/Shared.Infrastructure.Core/Shared.Infrastructure.Core.csproj", "Shared/Shared.Infrastructure.Core/"]
COPY ["Shared/Shared.Doamin.Abstractions/Shared.Domain.Abstractions.csproj", "Shared/Shared.Doamin.Abstractions/"]
RUN dotnet restore "Services/Identity/NMS.Identity.Web.csproj"
COPY . .
WORKDIR "/src/Services/Identity"
RUN dotnet build "NMS.Identity.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NMS.Identity.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NMS.Identity.Web.dll"]