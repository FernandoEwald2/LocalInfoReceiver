using Newtonsoft.Json;

namespace Api.Exceptions
{
    public class ExceptionMessage
    {
        [JsonProperty("codigo")]
        public string Codigo { get; set; } = "";

        [JsonProperty ("mensagem")]
        public string Mensagem { get; set; } = "";
    }
}
