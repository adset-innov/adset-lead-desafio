
using AdSetLead.Core.Interfaces.IRepository;
using AdSetLead.Core.Model;
using AdSetLead.Core.Responses;
using AdSetLead.Data.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;


namespace AdSetLead.Data.Reposiotry
{
    public class MarcaRepository : IMarcaRepository
    {
        private readonly ApplicationDbContext dbContext = new ApplicationDbContext();

        /// <summary>
        /// Atualiza marca de carro
        /// </summary>
        /// <param name="marca">Marca de carro a ser atualizado</param>
        /// <returns>Marca de marca atualizado</returns>
        public MarcaResponse AtualizarMarcaDeCarro(Marca marca)
        {
            MarcaResponse carroResponse = new MarcaResponse();

            try
            {
                dbContext.Marca.AddOrUpdate(c => c.Id, marca);

                carroResponse.ResponseData.Add(marca);
                return carroResponse;
            }
            catch (Exception ex)
            {
                carroResponse.AddExceptionMessage("101", $"Erro de exceção: {ex.Message}");

                return carroResponse;
            }
        }

        /// <summary>
        /// Busca marca de carro por id
        /// </summary>
        /// <param name="id">id do marca</param>
        /// <returns>Response com a marca e seus relacionamentos</returns>
        public MarcaResponse BuscarMarcaPorId(int id)
        {
            MarcaResponse response = new MarcaResponse();

            try
            {  
                Marca marca = dbContext.Marca.Include<Marca>("Modelo").SingleOrDefault(c => c.Id == id);
                response.ResponseData.Add(marca);

                return response;
            }
            catch (Exception ex)
            {
                response.AddExceptionMessage("101", $"Erro de exceção: {ex.Message}");

                return response;
            }  
        }

        /// <summary>
        /// Retorna todas as marcas de carro
        /// </summary>
        /// <returns>Lista de marca de carro</returns>
        public MarcaResponse BuscarTodasMarcas()
        {
            MarcaResponse response = new MarcaResponse();

            try
            {
                List<Marca> marca = dbContext.Marca.Include("modelos").ToList();

                if (!marca.Any())
                {
                    response.AddErrorMessage("100", "Carros não encontrados ou algum erro inesperado aconteceu");
                }

                response.ResponseData.AddRange(marca);

                return response;
            }
            catch (Exception ex)
            {
                response.AddExceptionMessage("101", $"Erro de exceção: {ex.Message}");

                return response;
            }
        }

        /// <summary>
        /// Deletar a marca de carro
        /// </summary>
        /// <param name="id">id da marca</param>
        /// <returns>Marca Excluída</returns>
        public MarcaResponse DeletetarMarcadeCarroPorId(int id)
        {
            MarcaResponse response = new MarcaResponse();

            try
            {
                Marca marca = dbContext.Marca.Find(id);
                if (marca == null)
                {
                    response.AddWarningMessage("900", "Marca não encontrado");

                    return response;
                }

                dbContext.Marca.Remove(marca);
                dbContext.SaveChanges();

                response.ResponseData.Add(marca);

                return response;
            }
            catch (Exception ex)
            {
                response.AddExceptionMessage("101", $"Erro de exceção: {ex.Message}");

                return response;
            }
        }

        /// <summary>
        /// Registra marca de carro
        /// </summary>
        /// <param name="marca">Marca a ser registrado</param>
        /// <returns>Marca resgistrada</returns>
        public MarcaResponse RegistrarMarcaDeCarro(Marca marca)
        {
            MarcaResponse response = new MarcaResponse();

            try
            {
                // Não pode haver nome de marca duplicado
                bool marcaDublipaca = dbContext.Marca.Any(m => m.Nome == marca.Nome);

                if (marcaDublipaca)
                {
                    response.AddValidationMessage("910", $"A marca {marca.Nome} já existe");

                    return response;
                }

                Marca novoCarro = dbContext.Marca.Add(marca);
                int op = dbContext.SaveChanges();

                if (op == 0 || novoCarro == null)
                {
                    response.AddErrorMessage("911", "Marca não foi salvo");
                    return response;
                }

                response.ResponseData.Add(marca);

                return response;
            }
            catch (Exception ex)
            {
                response.AddExceptionMessage("101", $"Erro de exceção: {ex.Message}");

                return response;
            }
        }
    }
}
