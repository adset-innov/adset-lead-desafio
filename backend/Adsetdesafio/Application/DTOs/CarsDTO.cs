using Adsetdesafio.Shared.Utils.Enums;

namespace Adsetdesafio.Application.DTOs
{
    public class CarsDTO
    {
        public long Id { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public int Ano { get; set; }
        public string Placa { get; set; }
        public int? Km { get; set; }
        public EnumCor Cor { get; set; }
        public decimal Preco { get; set; }
        public List<EnumOptionsCar> OpcionaisVeiculo { get; set; } = new();
        public List<CategoryPortalDTO> PortaisCategorias { get; set; } = new();
    }
}
