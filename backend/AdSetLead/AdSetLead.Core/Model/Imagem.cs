
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdSetLead.Core.Models
{
    [Table(nameof(Imagem))]
    public class Imagem
    {
        [Key]
        public int Id { get; set; }
        public string Url { get; set; }
        public int CarroId { get; set; }

        [ForeignKey("CarroId")]
        public Carro Carro { get; set; }
    }
}
