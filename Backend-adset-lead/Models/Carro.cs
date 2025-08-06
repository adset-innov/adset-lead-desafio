using System.ComponentModel.DataAnnotations;

namespace Backend_adset_lead.Models
{
    public class Carro
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Marca { get; set; }
        [Required]
        public string? Modelo { get; set; }
        [Required]
        public int Ano { get; set; }
        [Required]
        public string? Placa { get; set; }        
        public int Quilometragem { get; set; }
        [Required]
        public string? Cor { get; set; }
        [Required]
        public decimal Preco { get; set; }
        public string? ListaOpcionais { get; set; }
        public List<PortalPacote> PortalPacotes { get; set; } = new();
        public List<Foto> Fotos { get; set; } = new();

    }
}
