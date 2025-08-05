using System.ComponentModel.DataAnnotations;

namespace Backend_adset_lead.Models
{
    public class Carro
    {
        [Key]
        public int Id { get; set; }
        [Required]
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
        public List<PortalPacote> PortalPackages { get; set; } = new();
        public List<Foto> Fotos { get; set; } = new();

    }
}
