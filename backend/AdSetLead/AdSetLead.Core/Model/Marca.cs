
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdSetLead.Core.Model
{
    [Table("Marca")]
    public class Marca
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        public List<Modelo> Modelos { get; set; }
    }
}
