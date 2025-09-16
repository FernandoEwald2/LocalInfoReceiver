# Base runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5000

# Build SDK
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Api/Api.csproj", "Api/"]
RUN dotnet restore "./Api/Api.csproj"
COPY . .
WORKDIR "/src/Api"
RUN dotnet build "./Api.csproj" -c Release -o /app/build

# Publish
FROM build AS publish
RUN dotnet publish "./Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Final image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Copia o start.sh
COPY start.sh /app/start.sh
RUN chmod +x /app/start.sh

# Instala dotnet-ef globalmente
RUN dotnet tool install --global dotnet-ef

# Variáveis de ambiente
ENV WT_SECRET="8as22uo61shI112)Ts0oOUSaoHssiiNRjshIh7Ga17ja77huo*iH0a800sTHaaij"
ENV DB_NAME="localzap"
ENV DB_USER="default"
ENV DB_PASSWORD="Ew@ldP@$$123"
ENV PATH="$PATH:/root/.dotnet/tools"

# Entrypoint: roda migrations e inicia API
ENTRYPOINT ["/bin/sh", "/app/start.sh"]

