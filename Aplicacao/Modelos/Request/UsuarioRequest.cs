namespace Aplicacao.Modelos
{
    public class UsuarioRequest
    {
        public string nome { get; set; } = string.Empty;
        public string login { get; set; } = string.Empty;
        public string senha { get; set; } = string.Empty;
        public string cpf { get; set; } = string.Empty;
        public string telefone { get; set; } = string.Empty;
        public string email { get; set; } = string.Empty;
        public int perfil { get; set; }


        public string cep { get; set; } = string.Empty;
        public string logradouro { get; set; } = string.Empty;
        public string bairro { get; set; } = string.Empty;
        public string numero { get; set; } = string.Empty;
        public string cidade { get; set; } = string.Empty;
        public string estado { get; set; } = string.Empty;
    }
}
