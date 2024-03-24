namespace GerenciarCarros.Application.Models
{
    public class ImagemModel
    {
        public Guid? Id { get; set; }
        public Guid? IdCarro { get; set; }
        public byte[] Bytes { get; set; }
        public string? Tipo { get; set; }
        public string? Nome { get; set; }
    }
}
