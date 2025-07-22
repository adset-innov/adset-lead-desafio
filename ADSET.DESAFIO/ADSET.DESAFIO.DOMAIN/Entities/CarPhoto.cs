using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ADSET.DESAFIO.DOMAIN.Entities
{
    [Table("tb_car_photo")]
    public class CarPhoto
    {
        [Key]
        [Column(name: "id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "CarId is required.")]
        [Column(name: "car_id")]
        public int CarId { get; set; }

        [Required(ErrorMessage = "Photo Data is required.")]
        [Column(name: "photo_data")]
        public byte[]? PhotoData { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Order cannot be negative.")]
        [Column(name: "order")]
        public int Order { get; set; }

        [ForeignKey(nameof(CarId))]
        public Car Car { get; set; } = new Car();

        public CarPhoto() { }

        public CarPhoto(int carId, byte[]? photoData, int order)
        {
            CarId = carId;
            PhotoData = photoData;
            Order = order;
        }
    }
}