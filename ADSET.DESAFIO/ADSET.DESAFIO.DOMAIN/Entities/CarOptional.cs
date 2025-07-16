using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ADSET.DESAFIO.DOMAIN.Entities
{
    [Table("tb_car_optional")]
    public class CarOptional
    {
        [Key]
        [Column(name: "car_id")]
        public int CarId { get; set; }

        [Key]
        [Column(name: "optional_id")]
        public int OptionalId { get; set; }

        [ForeignKey(nameof(CarId))]
        public Car Car { get; set; } = new Car();

        [ForeignKey(nameof(OptionalId))]
        public Optional Optional { get; set; } = new Optional();

        public CarOptional() { }

        public CarOptional(int carId, int optionalId)
        {
            CarId = carId;
            OptionalId = optionalId;
        }
    }
}