FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 7132
EXPOSE 7133
EXPOSE 7134

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["TestTask/*", "TestTask/"]
COPY ["TestTask.BLL/*", "TestTask.BLL/"]
COPY ["TestTask.DAL/*", "TestTask.DAL/"]
COPY ["TestTask.Domain/*", "TestTask.Domain/"]
RUN dotnet restore "./TestTask/TestTask.API.csproj"
RUN dotnet build "./TestTask/TestTask.API.csproj" -c $BUILD_CONFIGURATION -o /app/build
RUN dotnet dev-certs https -ep /https/aspnetapp.pfx -p passw0rd

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./TestTask/TestTask.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

RUN mkdir -p /app/certificates
COPY --chmod=0755 --from=build /https/* /app/certificates

ENTRYPOINT ["dotnet", "TestTask.API.dll"]