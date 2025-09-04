namespace adset_api.DTOs
{
    public class VeiculoResponseDTO
    {
        public int IdVeiculo { get; set; }
        public string? Marca { get; set; }
        public string? Modelo { get; set; }
        public int Ano { get; set; }
        public string? Placa { get; set; }
        public int Quilometragem { get; set; }
        public string? Cor { get; set; }
        public decimal Preco { get; set; }
        public string? ListaOpcionais { get; set; }
        public List<PacoteResponseDTO> PortalPacotes { get; set; } = new();
        public List<FotoResponseDTO> Fotos { get; set; } = new();
    }
}
