using API.Dtos.Pacote;

namespace API.Dtos.Portal
{
    public class DetalharPortalDto
    {
        public int Id { get; set; }
        public string Nome { get; set; } = null!;

        public List<PacoteDto>? Pacotes { get; set; }
    }
}
