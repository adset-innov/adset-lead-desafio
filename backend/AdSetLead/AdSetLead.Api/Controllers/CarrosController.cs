using System.Collections.Specialized;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using AdSetLead.Core.Interfaces.IRepository;
using AdSetLead.Core.Model;
using AdSetLead.Core.Models;
using AdSetLead.Core.Repository;
using AdSetLead.Core.Request;
using AdSetLead.Core.Responses;
using Unity;
using System.Collections.Generic;
using System.Threading.Tasks;
using AdSetLead.Core.Extentions;
using System.Web;
using System.Linq;
using AdSetLead.Core.Extensions;
using AdSetLead.Core.Bac;
using AdSetLead.Api.Services;
using AdSetLead.Core.Interfaces.IServices;
using Daptive.Share.Response;

namespace AdSetLead.Api.Controllers
{
    public class CarrosController : ApiController
    {
        private readonly ICarroRepository _carroRepository;
        private readonly IFotosService _fotosServices;

        public CarrosController()
        {
            UnityContainer container = new UnityContainer();
            container.RegisterType<ICarroRepository, CarroRepository>();
            container.RegisterType<IFotosService, FotosService>();

            _carroRepository = container.Resolve<ICarroRepository>();
            _fotosServices = container.Resolve<IFotosService>();
        }
            
        // GET: api/Carros
        [HttpGet]
        [Route("api/carros")]
        [ResponseType(typeof(CarroResponse))]
        public IHttpActionResult GetCarros()
        {
            HttpRequestMessage request = Request;
            NameValueCollection queryString = request.RequestUri.ParseQueryString();

            FilterRequest filtro = queryString.BuildFilterRequest();
            
            CarroResponse response = _carroRepository.BuscarCarrosPorRequest(filtro);
            if(response.HasErrorMessages || response.HasExceptionMessages)
            {
                HttpResponseMessage resultResponse = Request.CreateResponse(HttpStatusCode.BadRequest, response);

                return ResponseMessage(resultResponse);
            }

            return Ok(response);
        }

        [HttpGet]
        [Route("api/carros/ContarCarros")]
        [ResponseType(typeof(ContagemDeCarro))]
        public IHttpActionResult GetCarroCount()
        {
            ContagemDeCarro quantidadeCarros = _carroRepository.ContarTodosCarros();

            return Ok(quantidadeCarros);
        }

        [HttpGet]
        [Route("api/carros/cores")]
        [ResponseType(typeof(List<string>))]
        public IHttpActionResult GetTodasCoresCarro()
        {
            List<string> cores = _carroRepository.BuscarTodasCoresCarro();

            return Ok(cores);
        }

        // GET: api/Carros/5
        [HttpGet]
        [Route("api/carros/{id}")]
        [ResponseType(typeof(CarroResponse))]
        public IHttpActionResult GetCarro(int id)
        {
            CarroResponse response = _carroRepository.BuscarCarroPorId(id);
            if (response.HasAnyMessages)
            {
                HttpResponseMessage resultResponse = Request.CreateResponse(HttpStatusCode.BadRequest, response);
                return ResponseMessage(resultResponse);
            }

            return Ok(response);
        }

        // PUT: api/Carros
        [HttpPut]
        [Route("api/carros")]
        [ResponseType(typeof(ModelOperationalResponse))]
        public async Task<IHttpActionResult> PutCarro(Carro carro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ModelOperationalResponse response = await _carroRepository.AtualizarCarroAsync(carro);
            if (response.InError())
            {
                HttpResponseMessage resultResponse = Request.CreateResponse(HttpStatusCode.BadRequest, response);
                return ResponseMessage(resultResponse);
            }

            return Ok(response);
        }

        // POST: api/Carros
        [HttpPost]
        [Route("api/carros")]
        [ResponseType(typeof(ModelOperationalResponse))]
        public async Task<IHttpActionResult> PostCarro()
        {
            HttpRequest httpRequest = HttpContext.Current.Request;
            CarroBac carroBac = new CarroBac();

            CarroResponse response = carroBac.RegistrarCarroBac(httpRequest);
            if (response.InError())
            {
                HttpResponseMessage resultResponse = Request.CreateResponse(HttpStatusCode.BadRequest, response);
                return ResponseMessage(resultResponse);
            }
        
            ModelOperationalResponse newCarroResponse = await _carroRepository.RegistrarCarro(response.ResponseData.FirstOrDefault());
            if (response.InError())
            {
                HttpResponseMessage resultResponse = Request.CreateResponse(HttpStatusCode.BadRequest, response);
                return ResponseMessage(resultResponse);
            }            

            int novoCarroRegistradoId = response.ResponseData.Select(c => c.Id).SingleOrDefault();
            response = _carroRepository.BuscarCarroPorId(novoCarroRegistradoId);

            int fotosCount = response.ResponseData.Select(c => c.Imagens.Count()).SingleOrDefault();
            if (fotosCount >= 15)
            {
                response.AddWarningMessage("100", "Carro já possui limite máximo de fotos cadastradas");
                return Ok(response);
            }

            List<Imagem> imagens = _fotosServices.UploadFotos(httpRequest);
            Carro carroParaAtualizar = response.ResponseData.SingleOrDefault();
            carroParaAtualizar.Imagens.AddRange(imagens);

            newCarroResponse = await _carroRepository.AtualizarCarroAsync(carroParaAtualizar);

            return Ok(newCarroResponse);
        }

        // DELETE: api/Carros/5
        [HttpDelete]
        [Route("api/carros/{id}")]
        [ResponseType(typeof(CarroResponse))]
        public IHttpActionResult DeleteCarro(int id)
        {
            ModelOperationalResponse response = _carroRepository.DeletetarCarroPorId(id);
            if (response.InError())
            {
                HttpResponseMessage resultResponse = Request.CreateResponse(HttpStatusCode.BadRequest, response);
                return ResponseMessage(resultResponse);
            }

            return Ok(response);
        }
    }
}