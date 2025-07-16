using Adsetdesafio.Shared.Utils.Enums;

namespace Adsetdesafio.Domain.Models.Entities
{
    public class Car
    {
        public long Id { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public int Ano { get; set; }
        public string Placa { get; set; }
        public int? Km { get; set; }
        public EnumCor Cor { get; set; }
        public decimal Preco { get; set; }
        public List<EnumOptionsCar>? OpcionaisVeiculo { get; set; } = new();
        public List<string> Fotos { get; set; } = new();
        public List<AnnouncementPortal> PortaisAnuncio { get; set; } = new();
    }
}
