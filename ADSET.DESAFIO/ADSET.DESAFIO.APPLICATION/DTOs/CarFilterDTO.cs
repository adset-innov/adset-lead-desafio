using System.ComponentModel.DataAnnotations;

namespace ADSET.DESAFIO.APPLICATION.DTOs
{
    public class CarFilterDTO
    {
        [Range(2000, 2024, ErrorMessage = "YearMin must be between 2000 and 2024.")]
        public int? YearMin { get; set; }

        [Range(2000, 2024, ErrorMessage = "YearMax must be between 2000 and 2024.")]
        public int? YearMax { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "PriceMin cannot be negative.")]
        public decimal? PriceMin { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "PriceMax cannot be negative.")]
        public decimal? PriceMax { get; set; }

        public bool? HasPhotos { get; set; }

        [StringLength(30, ErrorMessage = "Color cannot exceed 30 characters.")]
        public string? Color { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (YearMin.HasValue && YearMax.HasValue && YearMin > YearMax)
            {
                yield return new ValidationResult("YearMin cannot be greater than YearMax.", new[] { nameof(YearMin), nameof(YearMax) });
            }
            if (PriceMin.HasValue && PriceMax.HasValue && PriceMin > PriceMax)
            {
                yield return new ValidationResult("PriceMin cannot be greater than PriceMax.", new[] { nameof(PriceMin), nameof(PriceMax) });
            }
        }
    }
}