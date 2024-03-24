namespace GerenciarCarros.Domain.Entities.Base
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataModificacao { get; private set; }

        public BaseEntity()
        {
            Id = Guid.NewGuid();
            DataCriacao = DateTime.Now;
        }
    }
}
