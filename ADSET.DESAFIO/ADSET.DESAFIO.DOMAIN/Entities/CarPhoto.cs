using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ADSET.DESAFIO.DOMAIN.Entities
{
    [Table("tb_car_photo")]
    public class CarPhoto
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "CarId is required.")]
        [Column("car_id")]
        public int CarId { get; set; }

        [Required(ErrorMessage = "Url is required.")]
        [Url(ErrorMessage = "Url format is invalid.")]
        [Column("url")]
        public string Url { get; set; } = string.Empty;

        [Range(0, int.MaxValue, ErrorMessage = "Order cannot be negative.")]
        [Column("order")]
        public int Order { get; set; }

        [ForeignKey(nameof(CarId))]
        public Car Car { get; set; } = new Car();

        public CarPhoto() { }

        public CarPhoto(int carId, string url, int order)
        {
            CarId = carId;
            Url = url;
            Order = order;
        }
    }
}