using System.ComponentModel.DataAnnotations;


namespace API.Dtos.Veiculo
{
    public class CadastrarVeiculoDto
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "A marca é obrigatória.")]
        [StringLength(250, ErrorMessage = "A marca não pode ter mais de 250 caracteres.")]
        public string Marca { get; set; } = null!;

        [Required(ErrorMessage = "O modelo é obrigatório.")]
        [StringLength(250, ErrorMessage = "O modelo não pode ter mais de 250 caracteres.")]
        public string Modelo { get; set; } = null!;

        [Required(ErrorMessage = "O ano é obrigatório.")]
        [Range(2000, 2024, ErrorMessage = "O ano deve estar entre 2000 e 2024.")]
        public int Ano { get; set; }

        [Required(ErrorMessage = "A placa é obrigatória.")]
        [StringLength(50, ErrorMessage = "A placa não pode ter mais de 50 caracteres.")]
        public string Placa { get; set; } = null!;

        public int? Km { get; set; }

        [Required(ErrorMessage = "A cor é obrigatória.")]
        [StringLength(250, ErrorMessage = "A cor não pode ter mais de 250 caracteres.")]
        public string Cor { get; set; } = null!;

        [Required(ErrorMessage = "O preço é obrigatório.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O preço deve ser maior que zero.")]
        public decimal Preco { get; set; }

        public List<int>? Opcionais { get; set; }

        public List<IFormFile>? Fotos { get; set; }

        public string? ErrosUpload { get; set; } = null!;

    }

}
