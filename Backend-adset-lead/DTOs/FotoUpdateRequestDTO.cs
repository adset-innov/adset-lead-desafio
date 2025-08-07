namespace Backend_adset_lead.DTOs
{
    public class FotoUpdateRequestDTO
    {
        public int Id { get; set; }
        public required string Url { get; set; }
        public int CarroId { get; set; }
    }
}
