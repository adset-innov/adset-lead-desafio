using System.ComponentModel.DataAnnotations;

namespace adset_api.Data
{
    public class Veiculo
    {
        [Key]
        public int IdVeiculo { get; set; }
        [Required]
        public string? Marca { get; set; }
        [Required]
        public string? Modelo { get; set; }
        [Required]
        public int Ano { get; set; }
        [Required]
        public string? Placa { get; set; }
        public int Quilometragem { get; set; }
        [Required]
        public string? Cor { get; set; }
        [Required]
        public decimal Preco { get; set; }
        public string? ListaOpcionais { get; set; }
        public List<Pacote> Pacote { get; set; } = new();
        public List<FotoVeiculo> FotosVeiculo { get; set; } = new();
    }
}
