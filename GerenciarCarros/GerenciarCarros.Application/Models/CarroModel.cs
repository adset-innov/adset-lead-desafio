namespace GerenciarCarros.Application.Models
{
    public class CarroModel
    {
        public Guid? Id { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public int Ano { get; set; }
        public string Placa { get; set; }
        public decimal Km { get; set; }
        public string Cor { get; set; }
        public decimal Preco { get; set; }
        public List<OpcionalModel>? Opcionais { get; set; }
        public List<ImagemModel>? Imagens { get; set; }
    }
}
