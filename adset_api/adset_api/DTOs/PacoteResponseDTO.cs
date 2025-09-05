using adset_api.Enum;

namespace adset_api.DTOs
{
    public class PacoteResponseDTO
    {
        public int IdPacote { get; set; }
        public int IdVeiculo { get; set; }
        public ETipoPacote TipoPacote { get; set; }
        public ETipoPortal TipoPortal { get; set; }
    }
}
