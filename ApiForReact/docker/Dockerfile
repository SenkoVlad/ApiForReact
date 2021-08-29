#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

ARG VERSION=5.0-alpine

FROM mcr.microsoft.com/dotnet/sdk:${VERSION} AS build
WORKDIR /app

COPY . .
RUN dotnet restore "ApiForReact.csproj" -r linux-musl-x64

FROM build AS publish
RUN dotnet publish \
    -c Release -o \
    -o /app/publish  \
    -r linux-musl-x64 \
    --self-contained=true \
    --no-restore \
    -p:PublishReadyToRun=true \
    -p:PublishTrimmed=true

FROM mcr.microsoft.com/dotnet/runtime-deps:${VERSION}
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 80

ENTRYPOINT ["./ApiForReact"]