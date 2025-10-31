namespace Aplicacao.Util
{
    public class Paginacao<T>
    {
        public List<T> Itens { get; set; }
        public int TotalDeRegistros { get; set; }
        public int TotalDePaginas { get; set; }
        public int PaginaAtual { get; set; }
        public int QuantidadePorPagina { get; set; }
        public Paginacao(List<T> itens, int totalRecords, int currentPage, int pageSize)
        {
            Itens = itens;
            TotalDeRegistros = totalRecords;
            QuantidadePorPagina = pageSize;
            PaginaAtual = currentPage;
            TotalDePaginas = (int)Math.Ceiling(totalRecords / (double)pageSize);
        }
    }
}
