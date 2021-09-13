#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["Services/User/NMS.Web/NMS.Patient.Web.csproj", "Services/User/NMS.Web/"]
COPY ["Services/User/NMS.Infrastructure/NMS.Patient.Infrastructure.csproj", "Services/User/NMS.Infrastructure/"]
COPY ["Services/User/NMS.Domain/NMS.Patient.Domain.csproj", "Services/User/NMS.Domain/"]
COPY ["Shared/Shared.Infrastructure.Core/Shared.Infrastructure.Core.csproj", "Shared/Shared.Infrastructure.Core/"]
COPY ["Shared/Shared.Doamin.Abstractions/Shared.Domain.Abstractions.csproj", "Shared/Shared.Doamin.Abstractions/"]
RUN dotnet restore "Services/User/NMS.Web/NMS.Patient.Web.csproj"
COPY . .
WORKDIR "/src/Services/User/NMS.Web"
RUN dotnet build "NMS.Patient.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NMS.Patient.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NMS.Patient.Web.dll"]
