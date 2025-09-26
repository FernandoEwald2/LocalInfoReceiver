using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Domain.Entities.Usuarios
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

        [Column("cep")]
        public string Cep { get; set; }

        [Column("logradouro")]
        public string Logradouro { get; set; }

        [Column("bairro")]
        public string Bairro { get; set; }

        [Column("numero")]
        public string Numero { get; set; }

        [Column("cidade")]
        public string Cidade { get; set; }

        [Column("uf")]
        public string Estado { get; set; }

        public Usuario() { }
        // Construtor
        public Usuario(string nome, string login, byte[] senhaHash, byte[] senhaSalt, string cep, string logradouro, string bairro, string numero, string cidade, string estado)
        {

            SetNome(nome);
            SetLogin(login);
            SetHash(senhaHash);
            SetSalt(senhaSalt);
            SetCep(cep);
            SetLogradouro(logradouro);
            Setbairro(bairro);
            SetNumero(numero);
            SetCidade(cidade);
            SetEstado(estado);
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


        public void AtualizarUsuario(Usuario usuario)
        {
            SetNome(usuario.Nome);
            SetLogin(usuario.Login);
            SetHash(usuario.SenhaHash);
            SetSalt(usuario.SenhaSalt);

        }

    }


}
