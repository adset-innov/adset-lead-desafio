using GerenciarCarros.Domain.Entities.Base;

namespace GerenciarCarros.Domain.Entities
{
    public sealed class Carro : BaseEntity
    {
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public int Ano { get; set; }
        public string Placa { get; set; }
        public decimal Km { get; set; }
        public string Cor { get; set; }
        public decimal Preco { get; set; }

        public IEnumerable<Opcionais>? Opcionais { get; set; }
        public IEnumerable<Imagem>? Imagens { get; set; }
        public IEnumerable<Pacote>? Pacotes { get; set; }
    }
}
