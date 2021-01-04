#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["Services/Identity/Identity.Web/Pet.Identity.Web.csproj", "Services/Identity/Identity.Web/"]
COPY ["Services/Identity/Identity.Infrastructure/Pet.Identity.Infrastructure.csproj", "Services/Identity/Identity.Infrastructure/"]
COPY ["Services/Identity/Identity.Core/Pet.Identity.Core.csproj", "Services/Identity/Identity.Core/"]
COPY ["Shared/Shared.Infrastructure.Core/Shared.Infrastructure.Core.csproj", "Shared/Shared.Infrastructure.Core/"]
COPY ["Shared/Shared.Doamin.Abstractions/Shared.Domain.Abstractions.csproj", "Shared/Shared.Doamin.Abstractions/"]
RUN dotnet restore "Services/Identity/Identity.Web/Pet.Identity.Web.csproj"
COPY . .
WORKDIR "/src/Services/Identity/Identity.Web"
RUN dotnet build "Pet.Identity.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Pet.Identity.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Pet.Identity.Web.dll"]
