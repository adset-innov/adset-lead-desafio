using Adsetdesafio.Shared.Utils.Enums;

namespace Adsetdesafio.Application.DTOs
{
    public class CarsFilterDTO
    {
        public int? AnoMin { get; set; }
        public int? AnoMax { get; set; }
        public long? Preco { get; set; } 
        public bool? SomenteComFotos { get; set; }
        public EnumCor? Cor { get; set; }
        public List<EnumOptionsCar>? Opcionais { get; set; }
        public Dictionary<string, EnumCategory>? PortaisECategorias { get; set; }
    }
}
