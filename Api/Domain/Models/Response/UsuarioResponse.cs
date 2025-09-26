using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Domain.Models.Response
{
    public class UsuarioResponse
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Login { get; set; }        
        public string Cep { get; set; }        
        public string Logradouro { get; set; }        
        public string Bairro { get; set; }   
        public string Numero { get; set; }
        public string Cidade { get; set; }      
        public string Estado { get; set; }
    }
}
