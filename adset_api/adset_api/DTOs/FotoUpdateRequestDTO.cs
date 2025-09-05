namespace adset_api.DTOs
{
    public class FotoUpdateRequestDTO
    {
        public int IdFotoVeiculo { get; set; }
        public required string CaminhoUrl { get; set; }
        public int IdVeiculo { get; set; }
    }
}
