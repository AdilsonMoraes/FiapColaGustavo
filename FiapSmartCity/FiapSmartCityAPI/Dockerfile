#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["FiapSmartCityAPI/FiapSmartCityAPI.csproj", "FiapSmartCityAPI/"]
COPY ["FiapSmartCityServices/FiapSmartCityServices.csproj", "FiapSmartCityServices/"]
COPY ["FiapSmartCityDomain/FiapSmartCityDomain.csproj", "FiapSmartCityDomain/"]
COPY ["FiapSmartCityInfrastructure/FiapSmartCityInfrastructure.csproj", "FiapSmartCityInfrastructure/"]
COPY ["FiapSmartCityIoC/FiapSmartCityIoC.csproj", "FiapSmartCityIoC/"]
RUN dotnet restore "FiapSmartCityAPI/FiapSmartCityAPI.csproj"
COPY . .
WORKDIR "/src/FiapSmartCityAPI"
RUN dotnet build "FiapSmartCityAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FiapSmartCityAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FiapSmartCityAPI.dll"]
