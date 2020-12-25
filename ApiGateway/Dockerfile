FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
COPY ..
ENTRYPOINT ["dotnet", "Pet.Web.dll"]
