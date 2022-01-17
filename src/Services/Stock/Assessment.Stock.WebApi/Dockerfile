#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Services/Stock/Assessment.Stock.WebApi/Assessment.Stock.WebApi.csproj", "Services/Stock/Assessment.Stock.WebApi/"]
COPY ["BuildingBlocks/Assessment.Enum/Assessment.Enum.csproj", "BuildingBlocks/Assessment.Enum/"]
COPY ["Services/Stock/Assessment.Stock.Business/Assessment.Stock.Business.csproj", "Services/Stock/Assessment.Stock.Business/"]
COPY ["BuildingBlocks/Assessment.Dto/Assessment.Dto.csproj", "BuildingBlocks/Assessment.Dto/"]
COPY ["Services/Stock/Assessment.Stock.Core/Assessment.Stock.Core.csproj", "Services/Stock/Assessment.Stock.Core/"]
COPY ["BuildingBlocks/Assessment.Common/Assessment.Common.csproj", "BuildingBlocks/Assessment.Common/"]
COPY ["Services/Stock/Assessment.Stock.DataAccess/Assessment.Stock.DataAccess.csproj", "Services/Stock/Assessment.Stock.DataAccess/"]
COPY ["Services/Stock/Assessment.Stock.Entities/Assessment.Stock.Entities.csproj", "Services/Stock/Assessment.Stock.Entities/"]
RUN dotnet restore "Services/Stock/Assessment.Stock.WebApi/Assessment.Stock.WebApi.csproj"
COPY . .
WORKDIR "/src/Services/Stock/Assessment.Stock.WebApi"
RUN dotnet build "Assessment.Stock.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Assessment.Stock.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Assessment.Stock.WebApi.dll"]
