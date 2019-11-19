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
    public class G1_N1_subjectController : ApiController
    {
        private DADEntities db = new DADEntities();

        // GET: api/G1_N1_subject
        public IQueryable<G1_N1_subject> GetG1_N1_subject()
        {
            return db.G1_N1_subject;
        }

        // GET: api/G1_N1_subject/5
        [ResponseType(typeof(G1_N1_subject))]
        public IHttpActionResult GetG1_N1_subject(string id)
        {
            G1_N1_subject g1_N1_subject = db.G1_N1_subject.Find(id);
            if (g1_N1_subject == null)
            {
                return NotFound();
            }

            return Ok(g1_N1_subject);
        }

        // PUT: api/G1_N1_subject/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutG1_N1_subject(string id, G1_N1_subject g1_N1_subject)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != g1_N1_subject.SubjectCode)
            {
                return BadRequest();
            }

            db.Entry(g1_N1_subject).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!G1_N1_subjectExists(id))
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

        // POST: api/G1_N1_subject
        [ResponseType(typeof(G1_N1_subject))]
        public IHttpActionResult PostG1_N1_subject(G1_N1_subject g1_N1_subject)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.G1_N1_subject.Add(g1_N1_subject);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (G1_N1_subjectExists(g1_N1_subject.SubjectCode))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = g1_N1_subject.SubjectCode }, g1_N1_subject);
        }

        // DELETE: api/G1_N1_subject/5
        [ResponseType(typeof(G1_N1_subject))]
        public IHttpActionResult DeleteG1_N1_subject(string id)
        {
            G1_N1_subject g1_N1_subject = db.G1_N1_subject.Find(id);
            if (g1_N1_subject == null)
            {
                return NotFound();
            }

            db.G1_N1_subject.Remove(g1_N1_subject);
            db.SaveChanges();

            return Ok(g1_N1_subject);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool G1_N1_subjectExists(string id)
        {
            return db.G1_N1_subject.Count(e => e.SubjectCode == id) > 0;
        }
    }
}