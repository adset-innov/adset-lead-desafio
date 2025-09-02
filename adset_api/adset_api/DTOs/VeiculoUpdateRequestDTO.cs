namespace adset_api.DTOs
{
    public class VeiculoUpdateRequestDTO
    {
        public required int IdVeiculo { get; set; }
        public required string Marca { get; set; }
        public required string Modelo { get; set; }
        public required int Ano { get; set; }
        public required string Placa { get; set; }
        public int Quilometragem { get; set; }
        public required string Cor { get; set; }
        public required decimal Preco { get; set; }
        public string? ListaOpcionais { get; set; }
        public List<PacoteUpdateRequestDTO> PortalPacotes { get; set; } = new();
        public List<FotoUpdateRequestDTO> Fotos { get; set; } = new();
    }
}