using Backend_adset_lead.Enuns;

namespace Backend_adset_lead.DTOs
{
    public class PacoteRequestDTO
    {
        public required Portal Portal { get; set; }
        public required Pacote Pacote { get; set; }
    }
}
