using adset_api.Enum;

namespace adset_api.DTOs
{
    public class PacoteUpdateRequestDTO
    {
        public int IdPacote { get; set; }
        public int IdVeiculo { get; set; }
        public required ETipoPacote TipoPacote { get; set; }
        public required ETipoPortal TipoPortal { get; set; }
    }
}
