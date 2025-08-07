using System.ComponentModel.DataAnnotations;

namespace Backend_adset_lead.Models
{
    public class Foto
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public required string Url { get; set; }

        [Required]
        public int CarroId { get; set; }

        public Carro Carro { get; set; } = null!;
    }
}
