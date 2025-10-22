namespace API.Dtos.Veiculo
{
    public class VeiculoFiltroDto
    {
        public int Pagina { get; set; } = 1;
        public int TamanhoPagina { get; set; } = 10;

        public string? Marca { get; set; }
        public string? Modelo { get; set; }
        public string? Opcional { get; set; }
        public string? Placa { get; set; }
        public int? AnoMin { get; set; }
        public int? AnoMax { get; set; }
        public string? Cor { get; set; }
        public bool? Fotos { get; set; }
        public decimal? PrecoMin { get; set; }
        public decimal? PrecoMax { get; set; }
    }
}
