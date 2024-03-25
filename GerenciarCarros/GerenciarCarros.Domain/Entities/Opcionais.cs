using GerenciarCarros.Domain.Entities.Base;

namespace GerenciarCarros.Domain.Entities
{
    public sealed class Opcionais: BaseEntity
    {
        public Guid IdCarro { get; set; }
        public string Descricao { get; set; }
        public Carro Carro { get; set; }
    }
}
