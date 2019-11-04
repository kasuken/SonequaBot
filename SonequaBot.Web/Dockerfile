FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["SonequaBot.Web/SonequaBot.Web.csproj", "SonequaBot.Web/"]
RUN dotnet restore "SonequaBot.Web/SonequaBot.Web.csproj"
COPY . .
WORKDIR "/src/SonequaBot.Web"
RUN dotnet build "SonequaBot.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SonequaBot.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SonequaBot.Web.dll"]