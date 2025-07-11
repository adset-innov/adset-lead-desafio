using Adsetdesafio.Shared.Utils.Enums;

namespace Adsetdesafio.Domain.Models.Entities
{
    public class Car
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public int Ano { get; set; }
        public string Placa { get; set; }
        public int? Km { get; set; }
        public string Cor { get; set; }
        public decimal Preco { get; set; }
        public List<EnumOptionsCar> OpcionaisVeiculo { get; set; } = new();
        public List<string> Fotos { get; set; } = new();
    }
}
