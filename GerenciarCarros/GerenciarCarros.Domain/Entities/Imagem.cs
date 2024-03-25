using GerenciarCarros.Domain.Entities.Base;

namespace GerenciarCarros.Domain.Entities
{
    public sealed class Imagem : BaseEntity
    {
        public Guid IdCarro { get; set; }
        public byte[] Bytes { get; set; }
        public string Tipo { get; set; }
        public string? Nome { get; set; }
        public Carro Carro { get; set; }
    }
}
