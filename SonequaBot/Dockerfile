FROM mcr.microsoft.com/dotnet/core/runtime:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:5.0 AS build
WORKDIR /src
COPY ["SonequaBot/SonequaBot.csproj", "SonequaBot/"]
RUN dotnet restore "SonequaBot/SonequaBot.csproj"
COPY . .
WORKDIR "/src/SonequaBot"
RUN dotnet build "SonequaBot.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SonequaBot.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SonequaBot.dll"]