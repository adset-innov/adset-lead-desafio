
using AdSetLead.Core.Model;
using AdSetLead.Core.Responses;

namespace AdSetLead.Core.Interfaces.IRepository
{
    public interface IMarcaRepository
    {
        /// <summary>
        /// Atualiza marca de carro
        /// </summary>
        /// <param name="marca">Marca de carro a ser atualizado</param>
        /// <returns>Marca de carro atualizado</returns>
        MarcaResponse AtualizarMarcaDeCarro(Marca marca);

        /// <summary>
        /// Busca marca de carro por id
        /// </summary>
        /// <param name="id">id da marca</param>
        /// <returns>Response com a marca e seus relacionamentos</returns>
        MarcaResponse BuscarMarcaPorId(int id);


        /// <summary>
        /// Retorna todas as marcas de carro
        /// </summary>
        /// <returns>Lista de marca de carro</returns>
        MarcaResponse BuscarTodasMarcas();

        /// <summary>
        /// Deletar a marca de carro
        /// </summary>
        /// <param name="id">id da marca</param>
        /// <returns>Marca Excluída</returns>
        MarcaResponse DeletetarMarcadeCarroPorId(int id);

        /// <summary>
        /// Registra marca de carro
        /// </summary>
        /// <param name="carro">Marca a ser registrado</param>
        /// <returns>Marca resgistrado</returns>
        MarcaResponse RegistrarMarcaDeCarro(Marca marca);
    }
}
