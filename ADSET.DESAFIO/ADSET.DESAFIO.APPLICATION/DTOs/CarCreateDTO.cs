using ADSET.DESAFIO.DOMAIN.Enums;
using Microsoft.AspNetCore.Http;

namespace ADSET.DESAFIO.APPLICATION.DTOs
{
    public class CarCreateDTO
    {
        public string Brand { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public int Year { get; set; }
        public string Plate { get; set; } = string.Empty;
        public int? Km { get; set; }
        public string Color { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public List<string>? Optionals { get; set; }
        public Dictionary<Portal, Package>? PortalPackages { get; set; }
        public IFormFile[]? Photos { get; set; }

        public CarCreateDTO() { }

        public CarCreateDTO(string brand, string model, int year, string plate, int? km, string color, decimal price,
                            List<string>? optionals = null, Dictionary<Portal, Package>? portalPackages = null, IFormFile[]? photos = null)
        {
            Brand = brand;
            Model = model;
            Year = year;
            Plate = plate;
            Km = km;
            Color = color;
            Price = price;
            Optionals = optionals ?? new List<string>();
            PortalPackages = portalPackages ?? new Dictionary<Portal, Package>();
            Photos = photos ?? Array.Empty<IFormFile>();
        }
    }
}