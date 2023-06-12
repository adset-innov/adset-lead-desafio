
namespace AdSetLead.Core.Request
{
    public class FilterRequest
    {
        public string Placa { get; set; }
        public int MarcaId { get; set; }
        public int ModeloId { get; set; }
        public int AnoMin { get; set; }
        public int AnoMax { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public double PrecoMin { get; set; }
        public double PrecoMax { get; set; }
        public bool TemFoto { get; set; }
        public bool TemOpcionais { get; set; }
        public string Cor { get; set; }
    }
}
