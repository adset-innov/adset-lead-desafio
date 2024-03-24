namespace GerenciarCarros.Application.Models
{
    public class PaginacaoCarroModel
    {
        public int TamanhoPagina { get; set; } = 10;
        public int NumeroPagina { get; set; } = 1;
        public string? Ordenacao { get; set; }
        public string? Marca { get; set; }
        public string? Modelo { get; set; }
        public string? Placa { get; set; }
        public string? Cor { get; set; }
        public decimal? Preco { get; set; }
        public int? AnoMin { get; set; }
        public int? AnoMax { get; set; }
        public string? Opcionais { get; set; }
    }
}
