FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /app

COPY . . 

RUN dotnet restore ./SonequaBot.sln

COPY . . 

WORKDIR /app/SonequaBot
RUN dotnet build SonequaBot.csproj -c Release -o /app/build

FROM build AS publish
RUN dotnet publish SonequaBot.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS runtime
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SonequaBot.dll"]