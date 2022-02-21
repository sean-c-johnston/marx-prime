FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

# Creates a non-root user with an explicit UID and adds permission to access the /app folder
# For more info, please refer to https://aka.ms/vscode-docker-dotnet-configure-containers
RUN adduser -u 5678 --disabled-password --gecos "" appuser && chown -R appuser /app
USER appuser

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["MarxPrime.App.csproj", "MarxPrime.App/"]
RUN dotnet restore "MarxPrime.App/MarxPrime.App.csproj"

WORKDIR "/src/MarxPrime.App"
COPY . .
RUN dotnet build "MarxPrime.App.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MarxPrime.App.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MarxPrime.App.dll"]
