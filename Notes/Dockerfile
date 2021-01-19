# syntax = docker/dockerfile:1.0-experimental
FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app

FROM threax/build-dotnet:5.0 AS build
WORKDIR /src
COPY . .
WORKDIR /src/Notes
RUN --mount=type=cache,target=node_modules,id=threax-build-notes npm install
RUN --mount=type=cache,target=/root/.nuget/packages,id=threax-build-dotnet-nuget dotnet restore Notes.csproj
RUN --mount=type=cache,target=/root/.nuget/packages,id=threax-build-dotnet-nuget dotnet build Notes.csproj -c Release -o /app
RUN --mount=type=cache,target=node_modules,id=threax-build-notes npm run clean
RUN --mount=type=cache,target=node_modules,id=threax-build-notes npm run build

FROM build AS publish
RUN --mount=type=cache,target=/root/.nuget/packages,id=threax-build-dotnet-nuget dotnet publish Notes.csproj -c Release -o /app

FROM base AS final
ENV DOTNET_CLI_TELEMETRY_OPTOUT=1
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Notes.dll"]
