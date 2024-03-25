namespace GerenciarCarros.Domain.Pagination
{
    public class PaginacaoList<T>
    {
        public int PaginaAtual { get; set; }
        public int TotalPaginas { get; set; }
        public int TamanhoPagina { get; set; }
        public int QuantidadeTotal { get; set; }
        public bool TemAnterior => PaginaAtual > 1;
        public bool TemProxima => TotalPaginas > PaginaAtual;

        public List<T> Items { get; set; }
        public PaginacaoList(List<T> items, int quantidade, int numeroPagina, int tamanhoPagina)
        {
            QuantidadeTotal = quantidade;
            TamanhoPagina = tamanhoPagina;
            PaginaAtual = numeroPagina;
            TotalPaginas = (int)Math.Ceiling((double)quantidade / tamanhoPagina);
            Items = items;
        }
    }
}
