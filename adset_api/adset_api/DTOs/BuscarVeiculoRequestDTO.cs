using adset_api.Enum;

namespace adset_api.DTOs
{
    public class BuscarVeiculoRequestDTO
    {
        public string? Placa { get; set; }
        public string? Marca { get; set; }
        public string? Modelo { get; set; }
        public int? AnoMin { get; set; }
        public int? AnoMax { get; set; }
        public decimal? PrecoMin { get; set; }
        public decimal? PrecoMax { get; set; }
        public bool? HasPhotos { get; set; }
        public ETipoPacote? PacoteIcarros { get; set; }
        public ETipoPacote? PacoteWebmotors { get; set; }
        public string? Opcionais { get; set; }
        public string? Cor { get; set; }
        
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 50;
    }
}
