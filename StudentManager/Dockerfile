ARG COMMIT_SHA_ARG=""
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY "StudentManager.csproj" "StudentManager/"
WORKDIR "/src/StudentManager"
RUN dotnet restore "StudentManager.csproj"
COPY . .
RUN dotnet build "StudentManager.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "StudentManager.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
# use the value to set the ENV var default
ARG COMMIT_SHA_ARG
ENV COMMIT_SHA=$COMMIT_SHA_ARG
ENTRYPOINT ["dotnet", "StudentManager.dll"]
