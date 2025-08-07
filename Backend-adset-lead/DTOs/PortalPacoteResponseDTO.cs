using Backend_adset_lead.Enuns;

namespace Backend_adset_lead.DTOs
{
    public class PortalPacoteResponseDTO
    {
        public int Id { get; set; }
        public int CarroId { get; set; }
        public Portal Portal { get; set; }
        public Pacote Pacote { get; set; }
    }
}
