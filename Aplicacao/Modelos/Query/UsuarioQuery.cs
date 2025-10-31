namespace Aplicacao.Modelos.Query
{
    public class UsuarioQuery
    {
        public string busca { get; set; } = string.Empty;
        public int pagina { get; set; } = 1;
        public int quantidade_por_pagina { get; set; } = 10;

    }
}
