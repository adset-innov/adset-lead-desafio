using System.ComponentModel.DataAnnotations;

namespace Backend_adset_lead.DTOs
{
    public class NovoCarroRequestDTO
    {
        [Required(ErrorMessage = "O campo Marca é obrigatório")]
        public required string Marca { get; set; }
        [Required]
        public required string Modelo { get; set; }
        [Required]
        public required int Ano { get; set; }
        [Required]
        public required string Placa { get; set; }
        public int Quilometragem { get; set; }
        [Required]
        public required string Cor { get; set; }
        [Required]
        public required decimal Preco { get; set; }
        public string? ListaOpcionais { get; set; }
        [Required]
        public required List<PacoteRequestDTO> PortalPackages { get; set; } = new();
        public List<FotoRequestDTO> Fotos { get; set; } = new();

    }
}
