using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using G1_N1.Models;

namespace G1_N1.Controllers
{
    public class G1_N1_ClassController : ApiController
    {
        private DADEntities db = new DADEntities();

        // GET: api/G1_N1_Class
        public IQueryable<G1_N1_Class> GetG1_N1_Class()
        {
            return db.G1_N1_Class;
        }

        // GET: api/G1_N1_Class/5
        [ResponseType(typeof(G1_N1_Class))]
        public IHttpActionResult GetG1_N1_Class(int id)
        {
            G1_N1_Class g1_N1_Class = db.G1_N1_Class.Find(id);
            if (g1_N1_Class == null)
            {
                return NotFound();
            }

            return Ok(g1_N1_Class);
        }

        // PUT: api/G1_N1_Class/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutG1_N1_Class(int id, G1_N1_Class g1_N1_Class)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != g1_N1_Class.TeacherID)
            {
                return BadRequest();
            }

            db.Entry(g1_N1_Class).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!G1_N1_ClassExists(id))
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

        // POST: api/G1_N1_Class
        [ResponseType(typeof(G1_N1_Class))]
        public IHttpActionResult PostG1_N1_Class(G1_N1_Class g1_N1_Class)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.G1_N1_Class.Add(g1_N1_Class);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (G1_N1_ClassExists(g1_N1_Class.TeacherID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = g1_N1_Class.TeacherID }, g1_N1_Class);
        }

        // DELETE: api/G1_N1_Class/5
        [ResponseType(typeof(G1_N1_Class))]
        public IHttpActionResult DeleteG1_N1_Class(int id)
        {
            G1_N1_Class g1_N1_Class = db.G1_N1_Class.Find(id);
            if (g1_N1_Class == null)
            {
                return NotFound();
            }

            db.G1_N1_Class.Remove(g1_N1_Class);
            db.SaveChanges();

            return Ok(g1_N1_Class);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool G1_N1_ClassExists(int id)
        {
            return db.G1_N1_Class.Count(e => e.TeacherID == id) > 0;
        }
    }
}