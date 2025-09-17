using Api.Data;
using Api.Services.Interfaces;
using Api.Services.Models;
using Api.Util;

namespace Api.Services
{
    public class AuthService : IAuthService
    {
        private readonly LocalDbContext _context;
        private readonly IConfiguration _configuration;


        public AuthService(LocalDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public Autenticacao Autenticar(Auth auth)
        {
            // Verifica se as credenciais foram informadas
            if (string.IsNullOrWhiteSpace(auth.Login) || string.IsNullOrWhiteSpace(auth.Senha))
                throw new Exception("Credenciais inválidas");

            // Busca o usuário no banco de dados
            var usuario = _context.Usuarios.Where(u => u.Login.ToUpper().Equals(auth.Login.ToUpper())).FirstOrDefault();

            if (usuario == null)
                throw new Exception("Usuário não encontrado");

            // Verifica se a senha está correta
            if (!Criptografia.VerificarHashSalt(auth.Senha, usuario.SenhaHash, usuario.SenhaSalt))
                throw new Exception("Credenciais inválidas");

            // Configurações de expiração do token
            var expiresIn = int.Parse(_configuration["TokenSetting:Seconds"] ?? "86400"); // Default 24h
            var secret = Environment.GetEnvironmentVariable("JWT_SECRET") ?? _configuration["TokenSetting:Secret"];
            var issuer = _configuration["TokenSetting:Issuer"];

            // Verifica se a chave secreta foi configurada
            if (string.IsNullOrEmpty(secret))
                throw new Exception("Erro interno de autenticação");


            // Retorna o objeto de autenticação com o token gerado
            return new Autenticacao()
            {
                UsuarioName = usuario.Nome,
                UsuarioId = usuario.Id,
                ExpiraEm = DateTime.UtcNow.AddSeconds(expiresIn),
                AccessToken = Jwt.GenerateToken(usuario, secret)
            };
        }

    }


}
