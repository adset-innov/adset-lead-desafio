using adset_api.Enum;

namespace adset_api.DTOs
{
    public class PacoteRequestDTO
    {
        public required ETipoPacote TipoPacote { get; set; }
        public required ETipoPortal TipoPortal { get; set; }
    }
}
