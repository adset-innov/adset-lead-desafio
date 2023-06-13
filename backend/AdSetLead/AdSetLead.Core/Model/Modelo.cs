
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AdSetLead.Core.Model
{
    [Table(nameof(Modelo))]
    public class Modelo
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        [ForeignKey(nameof(Marca))]
        public int MarcaId { get; set; }

        public Marca Marca { get; set; }
    }
}
