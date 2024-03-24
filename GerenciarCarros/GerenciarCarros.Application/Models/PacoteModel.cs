using GerenciarCarros.Domain.Entities.Enums;

namespace GerenciarCarros.Application.Models
{
    public class PacoteModel
    {
        public Guid? Id { get; set; }
        public Guid? IdCarro { get; set; }
        public TipoPacote TipoPacote { get; set; }
    }
}
