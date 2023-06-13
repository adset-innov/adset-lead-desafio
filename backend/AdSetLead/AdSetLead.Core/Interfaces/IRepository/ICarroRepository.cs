
using AdSetLead.Core.Model;
using AdSetLead.Core.Models;
using AdSetLead.Core.Request;
using AdSetLead.Core.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdSetLead.Core.Interfaces.IRepository
{
    public interface ICarroRepository
    {
        /// <summary>
        /// Atualiza carro
        /// </summary>
        /// <param name="carro">Carro a ser atualizado</param>
        /// <returns>Carro atualizado</returns>
        Task<CarroResponse> AtualizarCarroAsync(Carro carro);

        /// <summary>
        /// Busca carros por request
        /// </summary>
        /// <param name="filterRequest"></param>
        /// <returns>Response com os carros</returns>
        CarroResponse BuscarCarrosPorRequest(FilterRequest filterRequest);

        /// <summary>
        /// Busca carro por id
        /// </summary>
        /// <param name="id">id do carro</param>
        /// <returns>Response com o carro e todos seu relacionamento</returns>
        CarroResponse BuscarCarroPorId(int id);

        /// <summary>
        /// Todas as cores dos carros cadastrados.
        /// </summary>
        /// <returns></returns>
        List<string> BuscarTodasCoresCarro();

        /// <summary>
        /// Faz contagem de carros
        /// Quantos carros cadastrados:
        /// Quantos carros cadastrados possui fotos,
        /// Quantos carros cadastrados não possui fotos,
        /// </summary>
        /// <returns>Response com as contagens</returns>
        ContagemDeCarro ContarTodosCarros();

        /// <summary>
        /// Deletar carro
        /// </summary>
        /// <param name="id">id do carro</param>
        /// <returns>Carro deletado</returns>
        CarroResponse DeletetarCarroPorId(int id);

        /// <summary>
        /// Registra carro
        /// </summary>
        /// <param name="carro">Carro a ser registrado</param>
        /// <returns>Carro resgistrado</returns>
        Task<CarroResponse> RegistrarCarro(Carro carro);
    }
}
