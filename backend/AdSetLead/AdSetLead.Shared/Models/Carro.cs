
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AdSetLead.Shared.Models
{
    public class Carro
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Marca { get; set; }

        [Required]
        public string Modelo { get; set; }

        [Required]
        public string Ano { get; set; }

        [Required]
        public string Placa { get; set; } // Todo: Placa deveria ser um relacionamento? A placa pode ser substituida futuramente??
        public double Kilometragem { get; set; } // não obrigatorio
        public string Cor { get; set; }

        public ICollection<Image> Imagens { get; set; }

        // Todo: Lista de opcionais do carro, não obrigatório
    }
}
