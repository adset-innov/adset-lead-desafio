using API.Dtos.Foto;


namespace API.Services.Interfaces
{
    public interface IFotoService
    {
        Task<FotoDto> EditarFotoVeiculoAsync(int idFoto, IFormFile foto);
        Task DeletarFotoAsync(int idFoto);

        Task CadastrarFotos(int veiucloId, List<IFormFile> fotos);
    }
}
