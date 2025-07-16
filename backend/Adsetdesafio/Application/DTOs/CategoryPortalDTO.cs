using Adsetdesafio.Shared.Utils.Enums;

namespace Adsetdesafio.Application.DTOs
{
    public class CategoryPortalDTO
    {
            public string NomePortal { get; set; } = null!;
            public EnumCategory Categoria { get; set; }
    }
}
