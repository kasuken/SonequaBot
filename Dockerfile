FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /app

COPY SonequaBot/SonequaBot.csproj ./SonequaBot/
RUN dotnet restore SonequaBot/SonequaBot.csproj

COPY SonequaBot/. ./SonequaBot/
WORKDIR /app/SonequaBot
RUN dotnet build SonequaBot.csproj -c Release -o /app/build

FROM build AS publish
RUN dotnet publish SonequaBot.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/core/runtime:3.1-buster-slim AS runtime
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SonequaBot.dll"]