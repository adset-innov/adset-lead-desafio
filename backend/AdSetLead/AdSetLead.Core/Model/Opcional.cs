
using AdSetLead.Core.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdSetLead.Core.Model
{
    [Table("Opcional")]
    public class Opcional
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        public string Descricao { get; set; }
        public virtual ICollection<Carro> Carros { get; set; }
    }
}
