

using API.Dtos.Foto;
using API.Dtos.RelacaoVeiculoOpcional;
using API.Dtos.RelacaoVeiculoPacotePortal;

namespace API.Dtos.Veiculo
{
    public class VeiculoDto
    {

        public int Id { get; set; }
        public string Marca { get; set; } = null!;
        public string Modelo { get; set; } = null!;
        public int Ano { get; set; }
        public string Placa { get; set; } = null!;
        public int? Km { get; set; }
        public string Cor { get; set; } = null!;
        public decimal Preco { get; set; } 

        public List<FotoDto> Fotos { get; set; } = new();
        public List<RelacaoVeiculoOpcionalDto> Opcionais { get; set; } = new();
        public List<RelacaoVeiculoPacotePortalDto> PacotesPortal { get; set; } = new();

    }


}
