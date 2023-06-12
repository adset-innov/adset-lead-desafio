
using AdSetLead.Core.Interfaces.IRepository;
using AdSetLead.Core.Model;
using AdSetLead.Core.Models;
using AdSetLead.Core.Responses;
using AdSetLead.Data.Context;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Data.Entity.Migrations;
using AdSetLead.Core.Request;
using System.Data.Entity;
using System.Threading.Tasks;

namespace AdSetLead.Core.Repository
{
    public class CarroRepository : ICarroRepository
    {
        private readonly ApplicationDbContext dbContext = new ApplicationDbContext();

        /// <summary>
        /// Atualiza carro
        /// </summary>
        /// <param name="carro">Carro a ser atualizado</param>
        /// <returns>Carro atualizado</returns>
        public CarroResponse AtualizarCarro(Carro carro)
        {
            CarroResponse carroResponse = new CarroResponse();

            try
            {
                dbContext.Carro.AddOrUpdate(carro);

                // Evita que opcionais sejam adicionados
                foreach (Opcional opcional in carro.Opcionais.ToList())
                {
                    dbContext.Entry(opcional).State = EntityState.Unchanged;
                }

                dbContext.SaveChanges();

                carroResponse.ResponseData.Add(carro);
                return carroResponse;
            }
            catch (Exception ex)
            {
                carroResponse.AddExceptionMessage("101", $"Erro de exceção: {ex.Message}");

                return carroResponse;
            }
        }

        /// <summary>
        /// Busca carro por id
        /// </summary>
        /// <param name="id">id do carro</param>
        /// <returns>Response com o carro e todos seu relacionamento</returns>
        public CarroResponse BuscarCarroPorId(int id)
        {
            CarroResponse response = new CarroResponse();

            try
            {
                Carro carro = dbContext.Carro.Include("Marca").Include("Modelo").SingleOrDefault(c => c.Id == id);

                if (carro == null)
                {
                    response.AddWarningMessage("911", "Carro não encontrado");
                }

                response.ResponseData.Add(carro);

                return response;
            }
            catch (Exception ex)
            {
                response.AddExceptionMessage("101", $"Erro de exceção: {ex.Message}");

                return response;
            }            
        }

        /// <summary>
        /// Busca carros por request
        /// </summary>
        /// <returns>Response com os carros</returns>
        public CarroResponse BuscarCarrosPorRequest(FilterRequest filterRequest)
        {
            CarroResponse response = new CarroResponse(); 

            try
            {
                List<Carro> carros = new List<Carro>();

                // Busca o carro com seus relacionamentos
                IQueryable<Carro> query = dbContext.Carro.Include(c => c.Imagens).Include(nameof(Marca)).Include(nameof(Modelo));

                query = FilterCarros(query, filterRequest);
                int totalItems = query.Count();


                if (filterRequest.CurrentPage > 0 && filterRequest.PageSize > 0) 
                {
                    carros = query.ToList().OrderBy(c => c.Id).Skip((filterRequest.CurrentPage - 1) * filterRequest.PageSize).Take(filterRequest.PageSize).ToList();
                }
                else
                {
                    carros = query.ToList();
                }
                               

                // Faz a relação de muitos carros para muitos opcionais
                foreach (var carro in carros)
                {
                    carro.Opcionais = dbContext.Entry(carro).Collection(c => c.Opcionais).Query().ToList();
                }

                // Garante traz somente os itens relacionados                
                carros = carros.Select(c => new Carro
                {
                    Id = c.Id,
                    Ano = c.Ano,
                    Cor = c.Cor,
                    Kilometragem = c.Kilometragem,
                    MarcaId = c.MarcaId,
                    Marca = new Marca
                    {
                        Id = c.Marca.Id,
                        Nome = c.Marca.Nome
                    },
                    ModeloId = c.ModeloId,
                    Modelo = new Modelo
                    {
                        Id = c.Modelo.Id,
                        Nome = c.Modelo.Nome,
                        MarcaId = c.Modelo.MarcaId
                    },
                    Placa = c.Placa,
                    Preco = c.Preco,
                    Imagens = c.Imagens.Select(im => new Imagem
                    {
                        Id = im.Id,
                        Url = im.Url,
                    }).ToList(),
                    Opcionais = c.Opcionais.Select(op => new Opcional
                    {
                        Id = op.Id,
                        Nome = op.Nome,
                        Descricao = op.Descricao,
                    }).ToList(),                    
                }).ToList();                                

                if (!carros.Any())
                {
                    response.AddErrorMessage("100", "Carros não encontrados ou algum erro inesperado aconteceu");
                }

                response.ResponseData.AddRange(carros);
                response.TotalAvailableItems = totalItems;

                return response;
            }
            catch (Exception ex)
            {
                response.AddExceptionMessage("101", $"Erro de exceção: {ex.Message}");

                return response;
            }  
        }

        /// <summary>
        /// Faz contagem de carros:
        /// Quantos carros cadastrados,
        /// Quantos carros cadastrados possui fotos,
        /// Quantos carros cadastrados não possui fotos,
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Response com as contagens</returns>
        public ContagemDeCarro ContarTodosCarros()
        {
            ContagemDeCarro contagemDeCarro = new ContagemDeCarro()
            {
                TotaldeCarros = dbContext.Carro.Count(),
                TotalCarrosComPhotos = dbContext.Carro.Count(c => c.Imagens.Any()),
                TotalCarrosSemPhotos = dbContext.Carro.Count(c => !c.Imagens.Any()),
            };
         
            return contagemDeCarro;
        }

        /// <summary>
        /// Deletar carro
        /// </summary>
        /// <param name="id">id do carro</param>
        /// <returns>Carro deletado</returns>
        public CarroResponse DeletetarCarroPorId(int id)
        {
            CarroResponse response = new CarroResponse();

            try
            {
                Carro carro = dbContext.Carro.Include(nameof(Opcional)).SingleOrDefault();
                if (carro == null)
                {
                    response.AddWarningMessage("900", "Carro não encontrado");

                    return response;
                }

                dbContext.Carro.Remove(carro);
                dbContext.SaveChanges();

                response.ResponseData.Add(carro);

                return response;
            }
            catch (Exception ex)
            {
                response.AddExceptionMessage("101", $"Erro de exceção: {ex.Message}");

                return response;
            }
        }

        /// <summary>
        /// Registra carro
        /// </summary>
        /// <param name="carro">Carro a ser registrado</param>
        /// <returns>Carro registrado</returns>
        public async Task<CarroResponse> RegistrarCarro(Carro carro)
        {
            CarroResponse response = new CarroResponse();

            try
            {
                bool placaDublipaca = dbContext.Carro.Any(c => c.Placa == carro.Placa);

                if (placaDublipaca)
                {
                    response.AddValidationMessage("910", "Um carro com essa placa já existe");

                    return response;
                }
                    
                dbContext.Carro.Add(carro);         
                
                // Evita que opcionais sejam adicionados
                foreach (Opcional opcional in carro.Opcionais.ToList())
                {
                    dbContext.Entry(opcional).State = EntityState.Unchanged;
                }
   
                int op = await dbContext.SaveChangesAsync();         

                if (op == 0)
                {
                    response.AddErrorMessage("911", "Carro não foi salvo");
                    return response;
                }

                response.ResponseData.Add(carro);

                return response;
            }
            catch (Exception ex)
            {
                response.AddExceptionMessage("101", $"Erro de exceção: {ex.Message}");

                return response;
            }          
        }

        public List<string> BuscarTodasCoresCarro()
        {
            List<string> cores = dbContext.Carro.Select(c => c.Cor).Distinct().ToList();

            return cores;
        }

        /// <summary>
        /// Aplica o filtro conforme request
        /// </summary>
        /// <param name="query">Carros queryable</param>
        /// <param name="filterRequest">Request</param>
        /// <returns>Carros queryable</returns>
        private IQueryable<Carro> FilterCarros(IQueryable<Carro> query, FilterRequest filterRequest)
        {
            // Faz a filtragem de acordo com request
            if (!string.IsNullOrEmpty(filterRequest.Placa))
            {
                query = query.Where(c => c.Placa.Equals(filterRequest.Placa));
            }

            if (filterRequest.MarcaId > 0)
            {
                query = query.Where(c => c.MarcaId.Equals(filterRequest.MarcaId));
            }

            if (filterRequest.ModeloId > 0)
            {
                query = query.Where(c => c.ModeloId.Equals(filterRequest.ModeloId));
            }

            if (filterRequest.AnoMin > 0 || filterRequest.AnoMax > 0)
            {
                query = query.Where(c => c.Ano >= filterRequest.AnoMin && c.Ano <= filterRequest.AnoMax);
            }

            if (filterRequest.PrecoMin >= 0 && filterRequest.PrecoMax > 0)
            {
                query = query.Where(c => c.Preco >= filterRequest.PrecoMin && c.Ano <= filterRequest.PrecoMax);
            }

            if (filterRequest.PrecoMax >= 10000 && filterRequest.PrecoMax == 0)
            {
                query = query.Where(c => c.Preco >= filterRequest.PrecoMin);
            }

            if (filterRequest.TemFoto)
            {
                query = query.Where(c => c.Imagens.Any());
            }

            if (filterRequest.TemOpcionais) 
            {
                query = query.Where(c => c.Opcionais.Any());
            }

            if (!string.IsNullOrEmpty(filterRequest.Cor))
            {
                query = query.Where(c => c.Cor.Equals(filterRequest.Cor));
            }

            return query;
        }
    }
}
