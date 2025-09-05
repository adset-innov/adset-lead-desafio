namespace adset_api.DTOs
{
    public class ListaPaginasDTO<T>
    {
        public List<T>? Items { get; set; }
        public int TotalPaginas { get; set; }
        public int TotalCarrosCadastrados { get; set; }
        public int TotalCarrosFiltrados { get; set; }
        public int TotalCarrosComFotos { get; set; }
        public int TotalCarrosSemFotos { get; set; }
        public List<string>? Cores { get; set; }
    }
}
