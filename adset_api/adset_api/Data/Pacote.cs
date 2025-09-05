using adset_api.Enum;
using System.ComponentModel.DataAnnotations;

namespace adset_api.Data
{
    public class Pacote
    {
        [Key]
        public int IdPacote { get; set; }
        [Required]
        public int IdVeiculo { get; set; }
        public Veiculo Veiculo { get; set; } = null!;
        [Required]
        public ETipoPacote TipoPacote { get; set; }
        [Required]
        public ETipoPortal TipoPortal { get; set; }
    }
}
