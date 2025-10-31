
using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio.Entidades.Usuarios
{
    [Table("usuario")]
    public class Usuario : EntitiesBase
    {
        
        [Column("nome")]
        public string Nome { get; private set; }
        [Column("login")]
        public string Login { get; private set; }
        [Column("senha_hash")]
        public byte[] SenhaHash { get; private set; }
        [Column("senha_salt")]
        public byte[] SenhaSalt { get; private set; }

        [Column("perfil")]
        public PerfilUsuarioEnum Perfil { get; set; }

        [Column("cpf")]
        public string Cpf { get; private set; }

        [Column("telefone")]
        public string Telefone { get; private set; }

        [Column("email")]
        public string Email { get; private set; }

        [Column("cep")]
        public string Cep { get; set; }

        [Column("logradouro")]
        public string Logradouro { get; set; }

        [Column("numero")]
        public string Numero { get; set; }

        [Column("bairro")]
        public string Bairro { get; set; }       

        [Column("cidade")]
        public string Cidade { get; set; }

        [Column("uf")]
        public string Estado { get; set; }

        public Usuario()
        {
        }
        public Usuario(string nome, string login, byte[] senhaHash, byte[] senhaSalt, string cep, string logradouro, string bairro, string numero, string cidade, string estado, int perfil, string cpf, string telefone, string email)
        {

            SetNome(nome);
            SetLogin(login);
            SetHash(senhaHash);
            SetSalt(senhaSalt);
            SetCpf(cpf);
            SetTelefone(telefone);
            SetEmail(email);
            SetCep(cep);
            SetLogradouro(logradouro);
            Setbairro(bairro);
            SetNumero(numero);
            SetCidade(cidade);
            SetEstado(estado);
            SetPerfil(perfil);
        }

        public void SetEmail(string email)
        {
            Email = email;
        }

        public void SetTelefone(string telefone)
        {
            Telefone = telefone;
        }

        public void SetCpf(string cpf)
        {
            Cpf = cpf;
        }

        public void SetEstado(string estado)
        {
            Estado = estado;
        }

        public void SetCidade(string cidade)
        {
            Cidade = cidade;
        }

        public void SetNumero(string numero)
        {
            Numero = numero;
        }

        public void Setbairro(string bairro)
        {
            Bairro = bairro;
        }

        public void SetLogradouro(string logradouro)
        {
            Logradouro = logradouro;
        }

        public void SetCep(string cep)
        {
            Cep = cep;
        }

        public void SetSalt(byte[] senhaSalt)
        {
            SenhaSalt = senhaSalt;
        }

        public void SetHash(byte[] senhaHash)
        {
            SenhaHash = senhaHash;
        }

        public void SetLogin(string login)
        {
            Login = login;
        }

        public void SetNome(string nome)
        {
            Nome = nome;
        }

        public void SetPerfil(int perfil)
        {
            Perfil = (PerfilUsuarioEnum)perfil;
        }


        public void AtualizarUsuario(Usuario usuario)
        {
            if(!string.IsNullOrEmpty(usuario.Nome))
                SetNome(usuario.Nome.ToUpper());
            if(string.IsNullOrEmpty(usuario.Login))
                SetLogin(usuario.Login);
            if (!string.IsNullOrEmpty(usuario.Cep))
                SetCep(usuario.Cep);
            if (!string.IsNullOrEmpty(usuario.Logradouro))
                SetLogradouro(usuario.Logradouro);
            if (!string.IsNullOrEmpty(usuario.Bairro))
                Setbairro(usuario.Bairro);
            if (!string.IsNullOrEmpty(usuario.Numero))
                SetNumero(usuario.Numero);
            if (!string.IsNullOrEmpty(usuario.Cidade))
                SetCidade(usuario.Cidade);
            if (!string.IsNullOrEmpty(usuario.Estado))
                SetEstado(usuario.Estado);
            if (usuario.Perfil != null)
                SetPerfil((int)usuario.Perfil);
            if (!string.IsNullOrEmpty(usuario.Cpf))
                SetCpf(usuario.Cpf);
            if (!string.IsNullOrEmpty(usuario.Telefone))
                SetTelefone(usuario.Telefone);
            if (!string.IsNullOrEmpty(usuario.Email))
                SetEmail(usuario.Email);
        }
        public void SetSenha(byte[] hash, byte[] salt)
        {
            SenhaHash = hash;
            SenhaSalt = salt;
        }
    }
}
