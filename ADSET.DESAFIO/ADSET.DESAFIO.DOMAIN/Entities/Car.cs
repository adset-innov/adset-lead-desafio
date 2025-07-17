using ADSET.DESAFIO.DOMAIN.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ADSET.DESAFIO.DOMAIN.Entities
{
    [Table(name: "tb_car")]
    public class Car
    {
        [Key]
        [Column(name: "id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Brand is required.")]
        [StringLength(50, ErrorMessage = "Brand cannot exceed 50 characters.")]
        [Column(name: "brand")]
        public string Brand { get; set; } = string.Empty;

        [Required(ErrorMessage = "Model is required.")]
        [StringLength(50, ErrorMessage = "Model cannot exceed 50 characters.")]
        [Column(name: "model")]
        public string Model { get; set; } = string.Empty;

        [Required(ErrorMessage = "Year is required.")]
        [Range(2000, 2024, ErrorMessage = "Year must be between 2000 and 2024.")]
        [Column(name: "year")]
        public int Year { get; set; }

        [Required(ErrorMessage = "Plate is required.")]
        [RegularExpression(@"^[A-Z0-9-]+$", ErrorMessage = "Plate format is invalid.")]
        [Column(name: "plate")]
        public string Plate { get; set; } = string.Empty;

        [Range(0, int.MaxValue, ErrorMessage = "Kilometers cannot be negative.")]
        [Column(name: "km")]
        public int Km { get; set; }

        [Required(ErrorMessage = "Color is required.")]
        [StringLength(30, ErrorMessage = "Color cannot exceed 30 characters.")]
        [Column(name: "color")]
        public string Color { get; set; } = string.Empty;

        [Required(ErrorMessage = "Price is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero.")]
        [Column(name: "price", TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        public ICollection<CarOptional> Optionals { get; set; } = new List<CarOptional>();
        public ICollection<CarPhoto> Photos { get; set; } = new List<CarPhoto>();
        public ICollection<CarPortalPackage> PortalPackages { get; set; } = new List<CarPortalPackage>();

        public Car() { }

        public Car(string brand, string model, int year, string plate, int km, string color, decimal price)
        {
            Brand = brand;
            Model = model;
            Year = year;
            Plate = plate;
            Km = km;
            Color = color;
            Price = price;
            Optionals = new List<CarOptional>();
            Photos = new List<CarPhoto>();
            PortalPackages = new List<CarPortalPackage>();
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Photos != null && Photos.Count > 15)
                yield return new ValidationResult("No more than 15 photos allowed.", new[] { nameof(Photos) });

            if (PortalPackages != null)
            {
                IEnumerable<Portal> duplicatePortals = PortalPackages.GroupBy(p => p.Portal).Where(g => g.Count() > 1).Select(g => g.Key);

                foreach (Portal portal in duplicatePortals)
                {
                    yield return new ValidationResult($"Multiple packages assigned for portal {portal}.", new[] { nameof(PortalPackages) });
                }
            }
        }
    }
}