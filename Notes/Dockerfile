FROM mcr.microsoft.com/dotnet/core/aspnet:3.0 AS base
WORKDIR /app

FROM threax/build-dotnet:3.0.0 AS build
WORKDIR /src
COPY . .
WORKDIR /src/Notes
RUN npm install
RUN dotnet restore Notes.csproj
RUN dotnet build Notes.csproj -c Release -o /app
RUN npm run clean
RUN npm run build

FROM build AS publish
RUN dotnet publish Notes.csproj -c Release -o /app

FROM base AS final
ENV DOTNET_CLI_TELEMETRY_OPTOUT=1
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Notes.dll"]
