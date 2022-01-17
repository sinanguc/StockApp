#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
# Install NodeJs
RUN apt-get update && \
apt-get install -y wget && \
apt-get install -y gnupg2 && \
wget -qO- https://deb.nodesource.com/setup_12.x | bash - && \
apt-get install -y build-essential nodejs
# End Install
WORKDIR /src
COPY ["WebApps/Assessment.WebStockApp/Assessment.WebStockApp.csproj", "WebApps/Assessment.WebStockApp/"]
RUN dotnet restore "WebApps/Assessment.WebStockApp/Assessment.WebStockApp.csproj"
COPY . .
WORKDIR "/src/WebApps/Assessment.WebStockApp"
RUN dotnet build "Assessment.WebStockApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Assessment.WebStockApp.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Assessment.WebStockApp.dll"]