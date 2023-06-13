
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Http;
using System.Web.Http.Description;
using AdSetLead.Core.Models;
using AdSetLead.Data.Context;

namespace AdSetLead.Api.Controllers
{
    public class ImagemsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Imagems
        public IQueryable<Imagem> GetImagem()
        {
            return db.Imagem;
        }

        // GET: api/Imagems/5
        [HttpGet]
        [Route("api/imagems/{fileName}")]
        [ResponseType(typeof(Imagem))]
        public async Task<IHttpActionResult> GetImagem(string fileName)
        {
            // Verifica se o nome do arquivo é válido
            if (string.IsNullOrEmpty(fileName))
            {
                return BadRequest("O nome do arquivo é inválido.");
            }

            // Define o diretório onde as fotos estão salvas
            var diretorio = HttpContext.Current.Server.MapPath("~/Fotos");

            // Obtém o caminho completo do arquivo
            var filePath = Path.Combine(diretorio, fileName);

            // Verifica se o arquivo existe
            if (!File.Exists(filePath))
            {
                return NotFound();
            }

            // Cria uma resposta HTTP com o arquivo físico
            var response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new StreamContent(File.OpenRead(filePath));
            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/jpeg"); // Defina o tipo de conteúdo correto

            return ResponseMessage(response);
        }

        // PUT: api/Imagems/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutImagem(int id, Imagem imagem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != imagem.Id)
            {
                return BadRequest();
            }

            db.Entry(imagem).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ImagemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Imagems
        [ResponseType(typeof(Imagem))]
        public async Task<IHttpActionResult> PostImagem()
        {
            try
            {
                /*
                var carro = await db.Carro.FindAsync(2);
                if (carro == null)
                {
                    return NotFound();
                }
                */

                var httpRequest = HttpContext.Current.Request;
                if (httpRequest.Files.Count == 0)
                {
                    return BadRequest("Nenhuma foto enviada.");
                }
                /*
                var fotosCount = carro.Imagens.Count;
                if (fotosCount >= 15)
                {
                    return BadRequest("O número máximo de fotos para este carro foi atingido.");
                }
                */

                var uploadDir = Path.Combine(HostingEnvironment.MapPath("~/Uploads"));
                if (!Directory.Exists(uploadDir))
                {
                    Directory.CreateDirectory(uploadDir);
                }

                var fotoFile = httpRequest.Files[0];
                var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(fotoFile.FileName);
                var filePath = Path.Combine(uploadDir, uniqueFileName);

                fotoFile.SaveAs(filePath);

                /*
                var fotoEntity = new Imagem
                {
                    Nome = uniqueFileName,
                    Url = filePath,
                    CarroId = carro.Id
                };
                */

                //_context.Fotos.Add(fotoEntity);
                //await _context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ImagemExists(int id)
        {
            return db.Imagem.Count(e => e.Id == id) > 0;
        }
    }
}