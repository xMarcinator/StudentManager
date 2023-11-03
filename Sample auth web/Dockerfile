FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Sample auth web/Sample auth web.csproj", "Sample auth web/"]
RUN dotnet restore "Sample auth web/Sample auth web.csproj"
COPY . .
WORKDIR "/src/Sample auth web"
RUN dotnet build "Sample auth web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Sample auth web.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Sample auth web.dll"]
