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
    public class G1_N1_TeacherController : ApiController
    {
        private DADEntities db = new DADEntities();

        // GET: api/G1_N1_Teacher
        public IQueryable<G1_N1_Teacher> GetG1_N1_Teacher()
        {
            return db.G1_N1_Teacher;
        }

        // GET: api/G1_N1_Teacher/5
        [ResponseType(typeof(G1_N1_Teacher))]
        public IHttpActionResult GetG1_N1_Teacher(int id)
        {
            G1_N1_Teacher g1_N1_Teacher = db.G1_N1_Teacher.Find(id);
            if (g1_N1_Teacher == null)
            {
                return NotFound();
            }

            return Ok(g1_N1_Teacher);
        }

        // PUT: api/G1_N1_Teacher/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutG1_N1_Teacher(int id, G1_N1_Teacher g1_N1_Teacher)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != g1_N1_Teacher.TeacherID)
            {
                return BadRequest();
            }

            db.Entry(g1_N1_Teacher).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!G1_N1_TeacherExists(id))
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

        // POST: api/G1_N1_Teacher
        [ResponseType(typeof(G1_N1_Teacher))]
        public IHttpActionResult PostG1_N1_Teacher(G1_N1_Teacher g1_N1_Teacher)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.G1_N1_Teacher.Add(g1_N1_Teacher);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (G1_N1_TeacherExists(g1_N1_Teacher.TeacherID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = g1_N1_Teacher.TeacherID }, g1_N1_Teacher);
        }

        // DELETE: api/G1_N1_Teacher/5
        [ResponseType(typeof(G1_N1_Teacher))]
        public IHttpActionResult DeleteG1_N1_Teacher(int id)
        {
            G1_N1_Teacher g1_N1_Teacher = db.G1_N1_Teacher.Find(id);
            if (g1_N1_Teacher == null)
            {
                return NotFound();
            }

            db.G1_N1_Teacher.Remove(g1_N1_Teacher);
            db.SaveChanges();

            return Ok(g1_N1_Teacher);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool G1_N1_TeacherExists(int id)
        {
            return db.G1_N1_Teacher.Count(e => e.TeacherID == id) > 0;
        }
    }
}