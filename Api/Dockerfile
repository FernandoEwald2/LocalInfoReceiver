ROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Instalar git
RUN apt-get update && apt-get install -y git

# Clonar o repositório
RUN git clone https://github.com/FernandoEwald2/LocalInfoReceiver.git /app

# Definir diretório de trabalho
WORKDIR /app/Api

# Instalar ferramenta Entity Framework globalmente
RUN dotnet tool install --global dotnet-ef

# Adicionar ferramentas ao PATH
ENV PATH="${PATH}:/root/.dotnet/tools"

# Instalar pacotes NuGet específicos
RUN dotnet add package Microsoft.EntityFrameworkCore.Tools --version 8.0.20
RUN dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer --version 8.0.20
RUN dotnet add package Microsoft.EntityFrameworkCore --version 8.0.20
RUN dotnet add package Microsoft.EntityFrameworkCore.Design --version 8.0.20
RUN dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL --version 8.0.11

# Restaurar dependências
RUN dotnet restore

# Aguardar o banco de dados estar pronto e executar migrations
COPY <<EOF /app/wait-and-migrate.sh
#!/bin/bash
echo "Aguardando banco de dados..."
sleep 10

echo "Executando migrations..."
cd /app/Api
dotnet ef migrations add InitialCreate --force
dotnet ef database update

echo "Iniciando aplicação..."
dotnet run --urls http://0.0.0.0:8080
EOF

RUN chmod +x /app/wait-and-migrate.sh

EXPOSE 8080

# Executar script que aguarda DB e roda migrations
CMD ["/app/wait-and-migrate.sh"]
