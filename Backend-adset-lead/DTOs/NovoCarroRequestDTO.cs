using System.ComponentModel.DataAnnotations;

namespace Backend_adset_lead.DTOs
{
    public class NovoCarroRequestDTO
    {
        public required string Marca { get; set; }
        public required string Modelo { get; set; }
        public required int Ano { get; set; }
        public required string Placa { get; set; }
        public int Quilometragem { get; set; }
        public required string Cor { get; set; }
        public required decimal Preco { get; set; }
        public string? ListaOpcionais { get; set; }
        public required List<PacoteRequestDTO> PortalPacotes { get; set; } = new();
        public List<FotoRequestDTO> Fotos { get; set; } = new();

    }
}
