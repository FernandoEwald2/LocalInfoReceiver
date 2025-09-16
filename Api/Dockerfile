# Base runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5000

# Build SDK
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Clona o repositório usando token passado como ARG
ARG GITHUB_TOKEN
RUN git clone https://$GITHUB_TOKEN@github.com/FernandoEwald2/LocalInfoReceiver.git .

WORKDIR /src/Api
RUN dotnet restore "./Api.csproj"
RUN dotnet build "./Api.csproj" -c Release -o /app/build
RUN dotnet publish "./Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Final image
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .

# Instala dotnet-ef
RUN dotnet tool install --global dotnet-ef
ENV PATH="$PATH:/root/.dotnet/tools"

# Variáveis de ambiente internas
ENV WT_SECRET="8as22uo61shI112)Ts0oOUSaoHssiiNRjshIh7Ga17ja77huo*iH0a800sTHaaij"
ENV DB_NAME="localzap"
ENV DB_USER="default"
ENV DB_PASSWORD="Ew@ldP@$$123"

# Entry point: roda migrations e inicia a API
ENTRYPOINT ["sh", "-c", "dotnet ef database update && dotnet Api.dll --urls http://0.0.0.0:5000"]
