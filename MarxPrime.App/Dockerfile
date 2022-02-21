FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["MarxPrime.App/MarxPrime.App.csproj", "MarxPrime.App/"]
RUN dotnet restore "MarxPrime.App/MarxPrime.App.csproj"
COPY . .
WORKDIR "/src/MarxPrime.App"
RUN dotnet build "MarxPrime.App.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MarxPrime.App.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MarxPrime.App.dll"]
