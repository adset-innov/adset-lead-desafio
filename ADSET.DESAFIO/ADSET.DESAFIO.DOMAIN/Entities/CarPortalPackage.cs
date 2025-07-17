using ADSET.DESAFIO.DOMAIN.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ADSET.DESAFIO.DOMAIN.Entities
{
    [Table("tb_car_portal_package")]
    public class CarPortalPackage
    {
        [Key]
        [Column(name: "car_id")]
        [Required(ErrorMessage = "CarId is mandatory.")]
        public int CarId { get; set; }

        [Required(ErrorMessage = "Portal is mandatory.")]
        [EnumDataType(typeof(Portal), ErrorMessage = "Invalid portal.")]
        [Column(name: "portal")]
        public Portal Portal { get; set; }

        [Required(ErrorMessage = "Package is mandatory.")]
        [EnumDataType(typeof(Package), ErrorMessage = "Invalid package.")]
        [Column(name: "package")]
        public Package Package { get; set; }

        [ForeignKey(nameof(CarId))]
        public Car Car { get; set; } = new Car();

        public CarPortalPackage() { }

        public CarPortalPackage(int carId, Portal portal, Package package)
        {
            CarId = carId;
            Portal = portal;
            Package = package;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!Enum.IsDefined(typeof(Portal), Portal))
            {
                yield return new ValidationResult("Portal value is not defined in the enum.", new[] { nameof(Portal) });
            }

            if (!Enum.IsDefined(typeof(Package), Package))
            {
                yield return new ValidationResult("Package value is not defined in the enum.", new[] { nameof(Package) });
            }
        }
    }
}