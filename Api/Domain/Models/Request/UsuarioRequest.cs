using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Domain.Models.Request
{
    public class UsuarioRequest
    {
        public string Nome { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }   
        public string Cep { get; set; }     
        public string Logradouro { get; set; }        
        public string Bairro { get; set; }      
        public string Numero { get; set; }     
        public string Cidade { get; set; }        
        public string Estado { get; set; }
    }
}
