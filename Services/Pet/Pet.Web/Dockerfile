#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["Services/User/Pet.Web/Pet.User.Web.csproj", "Services/User/Pet.Web/"]
COPY ["Services/User/Pet.Infrastructure/Pet.User.Infrastructure.csproj", "Services/User/Pet.Infrastructure/"]
COPY ["Services/User/Pet.Domain/Pet.User.Domain.csproj", "Services/User/Pet.Domain/"]
COPY ["Shared/Shared.Infrastructure.Core/Shared.Infrastructure.Core.csproj", "Shared/Shared.Infrastructure.Core/"]
COPY ["Shared/Shared.Doamin.Abstractions/Shared.Domain.Abstractions.csproj", "Shared/Shared.Doamin.Abstractions/"]
RUN dotnet restore "Services/User/Pet.Web/Pet.User.Web.csproj"
COPY . .
WORKDIR "/src/Services/User/Pet.Web"
RUN dotnet build "Pet.User.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Pet.User.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Pet.User.Web.dll"]
