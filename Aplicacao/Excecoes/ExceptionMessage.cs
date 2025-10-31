using Newtonsoft.Json;

namespace Aplicacao.Exceptions
{
    public class ExceptionMessage
    {
        [JsonProperty("codigo")]
        public string Codigo { get; set; } = "";

        [JsonProperty ("mensagem")]
        public string Mensagem { get; set; } = "";
    }
}
