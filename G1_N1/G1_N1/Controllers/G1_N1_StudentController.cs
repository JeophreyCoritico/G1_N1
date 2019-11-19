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
    public class G1_N1_StudentController : ApiController
    {
        private DADEntities db = new DADEntities();

        // GET: api/G1_N1_Student
        public IQueryable<G1_N1_Student> GetG1_N1_Student()
        {
            return db.G1_N1_Student;
        }

        // GET: api/G1_N1_Student/5
        [ResponseType(typeof(G1_N1_Student))]
        public IHttpActionResult GetG1_N1_Student(int id)
        {
            G1_N1_Student g1_N1_Student = db.G1_N1_Student.Find(id);
            if (g1_N1_Student == null)
            {
                return NotFound();
            }

            return Ok(g1_N1_Student);
        }

        // PUT: api/G1_N1_Student/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutG1_N1_Student(int id, G1_N1_Student g1_N1_Student)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != g1_N1_Student.Barcode)
            {
                return BadRequest();
            }

            db.Entry(g1_N1_Student).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!G1_N1_StudentExists(id))
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

        // POST: api/G1_N1_Student
        [ResponseType(typeof(G1_N1_Student))]
        public IHttpActionResult PostG1_N1_Student(G1_N1_Student g1_N1_Student)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.G1_N1_Student.Add(g1_N1_Student);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (G1_N1_StudentExists(g1_N1_Student.Barcode))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = g1_N1_Student.Barcode }, g1_N1_Student);
        }

        // DELETE: api/G1_N1_Student/5
        [ResponseType(typeof(G1_N1_Student))]
        public IHttpActionResult DeleteG1_N1_Student(int id)
        {
            G1_N1_Student g1_N1_Student = db.G1_N1_Student.Find(id);
            if (g1_N1_Student == null)
            {
                return NotFound();
            }

            db.G1_N1_Student.Remove(g1_N1_Student);
            db.SaveChanges();

            return Ok(g1_N1_Student);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool G1_N1_StudentExists(int id)
        {
            return db.G1_N1_Student.Count(e => e.Barcode == id) > 0;
        }
    }
}