using GerenciarCarros.Domain.Entities.Base;
using GerenciarCarros.Domain.Entities.Enums;

namespace GerenciarCarros.Domain.Entities
{
    public sealed class Pacote: BaseEntity
    {
        public Guid IdCarro { get; set; }
        public TipoPacote TipoPacote { get; set; }
        public Carro Carro { get; set; }
    }
}
