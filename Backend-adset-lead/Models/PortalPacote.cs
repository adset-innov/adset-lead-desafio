using Backend_adset_lead.Enuns;
using System.ComponentModel.DataAnnotations;

namespace Backend_adset_lead.Models
{
    public class PortalPacote
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public required int CarroId { get; set; }
        public Carro Carro { get; set; } = null!;
        [Required]
        public Portal Portal { get; set; }
        [Required]
        public Pacote Pacote { get; set; }
    }
}
