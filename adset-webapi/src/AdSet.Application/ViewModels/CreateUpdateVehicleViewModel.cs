using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace AdSet.Application.ViewModels
{
    public class CreateUpdateVehicleViewModel
    {
        [Required(ErrorMessage = "A placa é obrigatória.")]
        [StringLength(10, ErrorMessage = "A placa deve ter no máximo 10 caracteres.")]
        public string Plate { get; set; } = string.Empty;

        [Required(ErrorMessage = "O modelo é obrigatório.")]
        [StringLength(50, ErrorMessage = "O modelo deve ter no máximo 50 caracteres.")]
        public string Model { get; set; } = string.Empty;

        [Required(ErrorMessage = "A marca é obrigatória.")]
        [StringLength(50, ErrorMessage = "A marca deve ter no máximo 50 caracteres.")]
        public string Make { get; set; } = string.Empty;

        [Required(ErrorMessage = "O ano é obrigatório.")]
        [Range(1900, 2100, ErrorMessage = "O ano deve estar entre 1900 e 2100.")]
        public int Year { get; set; }

        [Required(ErrorMessage = "A cor é obrigatória.")]
        [StringLength(30, ErrorMessage = "A cor deve ter no máximo 30 caracteres.")]
        public string Color { get; set; } = string.Empty;

        [Required(ErrorMessage = "O preço é obrigatório.")]
        [Range(0.01, 999999999.99, ErrorMessage = "O preço deve ser maior que zero.")]
        public decimal Price { get; set; }

        public int? Km { get; set; }

        public IFormFileCollection? Images { get; set; }

        public List<int> Optionals { get; set; } = new List<int>();
    }
}
