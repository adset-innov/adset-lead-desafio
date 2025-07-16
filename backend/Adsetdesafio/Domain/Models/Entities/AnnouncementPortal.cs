using Adsetdesafio.Shared.Utils.Enums;

namespace Adsetdesafio.Domain.Models.Entities
{
    public class AnnouncementPortal
    {
        public long Id { get; set; }
        public long CarId { get; set; }
        public string NomePortal { get; set; } 
        public EnumCategory Categoria { get; set; }
    }
}
