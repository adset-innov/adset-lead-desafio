using Backend_adset_lead.Enuns;

namespace Backend_adset_lead.DTOs
{
    public class PacoteUpdateRequestDTO
    {
        public int Id { get; set; }
        public int CarroId { get; set; }
        public required Portal Portal { get; set; }
        public required Pacote Pacote { get; set; }
    }
}
