using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using AdSetLead.Core.Model;
using AdSetLead.Data.Context;

namespace AdSetLead.Api.Controllers
{
    public class OpcionalsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Opcionals
        [HttpGet]
        [Route("api/opcional")]
        [ResponseType(typeof(Opcional))]
        public IHttpActionResult GetOpcional()
        {
            List<Opcional> opcionais = db.Opcional.ToList();

            return Ok(opcionais);
        }

        // GET: api/Opcionals/5
        [ResponseType(typeof(Opcional))]
        public async Task<IHttpActionResult> GetOpcional(int id)
        {
            Opcional opcional = await db.Opcional.FindAsync(id);
            if (opcional == null)
            {
                return NotFound();
            }

            return Ok(opcional);
        }

        // PUT: api/Opcionals/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutOpcional(int id, Opcional opcional)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != opcional.Id)
            {
                return BadRequest();
            }

            db.Entry(opcional).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OpcionalExists(id))
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

        // POST: api/Opcionals
        [ResponseType(typeof(Opcional))]
        public async Task<IHttpActionResult> PostOpcional(Opcional opcional)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Opcional.Add(opcional);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = opcional.Id }, opcional);
        }

        // DELETE: api/Opcionals/5
        [ResponseType(typeof(Opcional))]
        public async Task<IHttpActionResult> DeleteOpcional(int id)
        {
            Opcional opcional = await db.Opcional.FindAsync(id);
            if (opcional == null)
            {
                return NotFound();
            }

            db.Opcional.Remove(opcional);
            await db.SaveChangesAsync();

            return Ok(opcional);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OpcionalExists(int id)
        {
            return db.Opcional.Count(e => e.Id == id) > 0;
        }
    }
}