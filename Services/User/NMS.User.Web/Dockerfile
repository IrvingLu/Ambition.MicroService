#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Services/User/NMS.User.Web/NMS.User.Web.csproj", "Services/User/NMS.User.Web/"]
COPY ["Services/User/NMS.User.Service/NMS.User.Service.csproj", "Services/User/NMS.User.Service/"]
COPY ["Services/User/NMS.User.Infrastructure/NMS.User.Infrastructure.csproj", "Services/User/NMS.User.Infrastructure/"]
COPY ["Services/User/NMS.User.Domain/NMS.User.Domain.csproj", "Services/User/NMS.User.Domain/"]
COPY ["Shared/Shared.Infrastructure.Core/Shared.Infrastructure.Core.csproj", "Shared/Shared.Infrastructure.Core/"]
COPY ["Shared/Shared.Doamin.Abstractions/Shared.Domain.Abstractions.csproj", "Shared/Shared.Doamin.Abstractions/"]
RUN dotnet restore "Services/User/NMS.User.Web/NMS.User.Web.csproj"
COPY . .
WORKDIR "/src/Services/User/NMS.User.Web"
RUN dotnet build "NMS.User.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NMS.User.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NMS.User.Web.dll"]