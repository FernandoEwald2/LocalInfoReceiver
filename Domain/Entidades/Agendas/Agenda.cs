
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Dominio.Entidades.Agendas
{
    [Table("agenda")]
    public class Agenda : EntitiesBase
    {
        [Column("nome")]
        public string Nome { get; set; }
        [Column("email")]
        public string Email { get; set; }
        [Column("telefone")]
        public string Telefone { get; set; }
        [Column("dt_nascimento")]
        public DateTime DataNascimento { get; set; }

        public Agenda()
        {
        }

        public Agenda(string nome, string email, string telefone, DateTime dataNascimento)
        {
            SetNome(nome);
            SetEmail(email);
            SetTelefone(telefone);
            SetDataNascimento(dataNascimento);
        }

        public void SetDataNascimento(DateTime dataNascimento)
        {
            DataNascimento = dataNascimento;
        }

        public void SetTelefone(string telefone)
        {
            Telefone = telefone;
        }

        public void SetEmail(string email)
        {
            Email = email;
        }

        public void SetNome(string nome)
        {
            Nome = nome;
        }

        public void AtualizarAgenda(Agenda agenda) 
        {
            if(!string.IsNullOrEmpty(agenda.Nome))
                SetNome(agenda.Nome.ToUpper());
            if(!string.IsNullOrEmpty(agenda.Email))
                SetEmail(agenda.Email);
            if(!string.IsNullOrEmpty(agenda.Telefone))
                SetTelefone(agenda.Telefone);
            if(agenda.DataNascimento != DateTime.MinValue && this.DataNascimento != agenda.DataNascimento)
                SetDataNascimento(agenda.DataNascimento);
        }
    }
}
