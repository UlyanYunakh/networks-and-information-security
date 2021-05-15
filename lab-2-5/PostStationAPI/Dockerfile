FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 5002

ENV ASPNETCORE_URLS=http://+:5002

FROM base AS final
WORKDIR /app
COPY publish .
ENTRYPOINT ["dotnet", "PostStationAPI.dll"]

# http://localhost:5002/user/github/login