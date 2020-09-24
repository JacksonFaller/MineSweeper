FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["MineSweeper.Web/MineSweeper.Web.API/MineSweeper.Web.API/MineSweeper.Web.API.csproj", "MineSweeper.Web/MineSweeper.Web.API/MineSweeper.Web.API/"]
COPY ["MineSweeper.Main/MineSweeper.csproj", "MineSweeper.Main/"]
COPY ["MineSweeper.Data/MineSweeper.Data.csproj", "MineSweeper.Data/"]
RUN dotnet restore "MineSweeper.Web/MineSweeper.Web.API/MineSweeper.Web.API/MineSweeper.Web.API.csproj"
COPY . .
WORKDIR "/src/MineSweeper.Web/MineSweeper.Web.API/MineSweeper.Web.API"
RUN dotnet build "MineSweeper.Web.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MineSweeper.Web.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MineSweeper.Web.API.dll"]