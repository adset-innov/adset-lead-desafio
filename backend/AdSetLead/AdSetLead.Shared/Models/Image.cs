
namespace AdSetLead.Shared.Models
{
    public class Image
    {
        public int Id { get; set; }
        public string Url { get; set; }

        public int CarroId { get; set; }

        public Carro Carro { get; set; }
    }
}
