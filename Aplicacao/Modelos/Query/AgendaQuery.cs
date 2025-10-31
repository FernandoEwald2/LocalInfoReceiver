namespace Aplicacao.Modelos.Query
{
    public class AgendaQuery
    {
        public string nome { get; set; } = string.Empty;
        public DateTime? data_nascimento { get; set; } = null;
        public int pagina { get; set; } = 1;
        public int quantidade_por_pagina { get; set; } = 10;
    }
}
