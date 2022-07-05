# Using multi stage builds for caching the project file information. project-files -> restore, so dotnet restore will
# be faster.
FROM mcr.microsoft.com/dotnet/sdk:6.0-bullseye-slim as project-files
WORKDIR /src
COPY . .
# Remove any file that is not a *.csproj file.
RUN find . -type f ! -name '*.csproj' -delete

FROM mcr.microsoft.com/dotnet/sdk:6.0-bullseye-slim AS restore
WORKDIR /src
COPY --from=project-files /src .

RUN dotnet restore "backend/backend.csproj"

FROM restore AS build
WORKDIR /src
COPY . .
COPY --from=restore /src .
WORKDIR "/src/backend"
RUN dotnet build --no-restore "backend.csproj" -c Release

FROM build as publish
WORKDIR /src
COPY --from=build /src .
WORKDIR "/src/backend"
RUN dotnet publish --no-build "backend.csproj" -c Release -o /app/publish

# Publish in its own isolated runtime stage, that only contains the built project files.
FROM mcr.microsoft.com/dotnet/aspnet:6.0-bullseye-slim AS run
EXPOSE 80
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "backend.dll"]
