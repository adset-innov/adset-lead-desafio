using System.ComponentModel.DataAnnotations;

namespace adset_api.Data
{
    public class FotoVeiculo
    {
        public int IdFoto { get; set; }

        [Required]
        public int IdVeiculo { get; set; }

        [Required]
        public required string CaminhoUrl { get; set; }

        public Veiculo Veiculo { get; set; } = null!;
    }
}
