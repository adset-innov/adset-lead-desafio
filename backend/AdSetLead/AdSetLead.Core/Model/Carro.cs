
using AdSetLead.Core.Model;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdSetLead.Core.Models
{
    [Table("Carro")]
    public class Carro
    {
        public Carro()
        {
            Opcionais = new List<Opcional>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public int Ano { get; set; }

        [Required]
        public string Cor { get; set; }

        public double? Kilometragem { get; set; }

        [Required]
        [ForeignKey(nameof(Marca))]
        public int MarcaId { get; set; }
        public Marca Marca { get; set; }

        [Required]
        [ForeignKey(nameof(Modelo))]
        public int ModeloId { get; set; }
        public Modelo Modelo { get; set; }

        [Required]
        public string Placa { get; set; }

        [Required]
        public double Preco { get; set; }     
      
        public ICollection<Imagem> Imagens { get; set; }

        public ICollection<Opcional> Opcionais { get; set; }

    }
}
