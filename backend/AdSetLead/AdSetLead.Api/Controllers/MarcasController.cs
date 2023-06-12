using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using AdSetLead.Core.Interfaces.IRepository;
using AdSetLead.Core.Model;
using AdSetLead.Core.Responses;
using AdSetLead.Data.Reposiotry;
using Unity;

namespace AdSetLead.Api.Controllers
{
    public class MarcasController : ApiController
    {
        private readonly IMarcaRepository _marcaRepository;

        public MarcasController()
        {
            UnityContainer container = new UnityContainer();
            container.RegisterType<IMarcaRepository, MarcaRepository>();
            _marcaRepository = container.Resolve<IMarcaRepository>();
        }

        // GET: api/Marcas
        [HttpGet]
        [Route("api/marcas")]
        [ResponseType(typeof(MarcaResponse))]
        public IHttpActionResult GetMarcas()
        {
            MarcaResponse response = _marcaRepository.BuscarTodasMarcas();
            if (response.HasAnyMessages)
            {
                HttpResponseMessage resultResponse = Request.CreateResponse(HttpStatusCode.BadRequest, response);

                return ResponseMessage(resultResponse);
            }

            return Ok(response);
        }

        // GET: api/Marcas/5
        [HttpGet]
        [Route("api/marcas/{id}")]
        [ResponseType(typeof(MarcaResponse))]
        public IHttpActionResult GetMarca(int id)
        {
           MarcaResponse response = _marcaRepository.BuscarMarcaPorId(id);
            if (response.HasAnyMessages)
            {
                HttpResponseMessage resultResponse = Request.CreateResponse(HttpStatusCode.BadRequest, response);

                return ResponseMessage(resultResponse);
            }

            return Ok(response);
        }

        // PUT: api/Marcas/5
        [HttpPut]
        [Route("api/marcas")]
        [ResponseType(typeof(MarcaResponse))]
        public IHttpActionResult PutMarca(Marca marca)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            MarcaResponse response = _marcaRepository.AtualizarMarcaDeCarro(marca);
            if (response.HasAnyMessages)
            {
                HttpResponseMessage resultResponse = Request.CreateResponse(HttpStatusCode.BadRequest, response);
                return ResponseMessage(resultResponse);
            }

            response.AddInfoMessage("200", "Carro atualizado com sucesso");

            return Ok(response);
        }

        // POST: api/Marcas
        [HttpPost]
        [Route("api/marcas")]
        [ResponseType(typeof(MarcaResponse))]
        public async Task<IHttpActionResult> PostMarca(Marca marca)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            MarcaResponse response =  _marcaRepository.RegistrarMarcaDeCarro(marca);
            if (response.HasAnyMessages)
            {
                HttpResponseMessage resultResponse = Request.CreateResponse(HttpStatusCode.BadRequest, response);
                return ResponseMessage(resultResponse);
            }

            response.AddInfoMessage("200", "Marca de carro creiada com sucesso");

            return Ok(response);
        }

        // DELETE: api/Marcas/5
        [HttpDelete]
        [Route("api/marcas/{id}")]
        [ResponseType(typeof(MarcaResponse))]
        public IHttpActionResult DeleteMarca(int id)
        {
            MarcaResponse response = _marcaRepository.DeletetarMarcadeCarroPorId(id);
            if (response.HasAnyMessages)
            {
                HttpResponseMessage resultResponse = Request.CreateResponse(HttpStatusCode.BadRequest, response);
                return ResponseMessage(resultResponse);
            }

            response.AddInfoMessage("200", "Excluído com sucesso");

            return Ok(response);
        }
    }
}