# Api do projeto CRM

Navegar até a pasta Infra
cd Infra (entrar na pasta do csproj)


Executar o comando a baixo para instalação do EF
dotnet tool install --global dotnet-ef

Executar o comendo abaixo para criar o migrations
dotnet ef migrations add InitialCreate

Executar esse comando para fazer o update no banco e atualizar as tabelas e ou criar
dotnet ef database update
