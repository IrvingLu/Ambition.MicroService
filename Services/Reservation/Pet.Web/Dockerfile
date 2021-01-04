#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["Services/Reservation/Pet.Web/Pet.Reservation.Web.csproj", "Services/Reservation/Pet.Web/"]
COPY ["Services/Reservation/Pet.Infrastructure/Pet.Reservation.Infrastructure.csproj", "Services/Reservation/Pet.Infrastructure/"]
COPY ["Services/Reservation/Pet.Domain/Pet.Reservation.Domain.csproj", "Services/Reservation/Pet.Domain/"]
COPY ["Shared/Shared.Infrastructure.Core/Shared.Infrastructure.Core.csproj", "Shared/Shared.Infrastructure.Core/"]
COPY ["Shared/Shared.Doamin.Abstractions/Shared.Domain.Abstractions.csproj", "Shared/Shared.Doamin.Abstractions/"]
RUN dotnet restore "Services/Reservation/Pet.Web/Pet.Reservation.Web.csproj"
COPY . .
WORKDIR "/src/Services/Reservation/Pet.Web"
RUN dotnet build "Pet.Reservation.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Pet.Reservation.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Pet.Reservation.Web.dll"]
