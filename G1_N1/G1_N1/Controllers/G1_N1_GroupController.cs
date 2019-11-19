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
    public class G1_N1_GroupController : ApiController
    {
        private DADEntities db = new DADEntities();

        // GET: api/G1_N1_Group
        public IQueryable<G1_N1_Group> GetG1_N1_Group()
        {
            return db.G1_N1_Group;
        }

        // GET: api/G1_N1_Group/5
        [ResponseType(typeof(G1_N1_Group))]
        public IHttpActionResult GetG1_N1_Group(int id)
        {
            G1_N1_Group g1_N1_Group = db.G1_N1_Group.Find(id);
            if (g1_N1_Group == null)
            {
                return NotFound();
            }

            return Ok(g1_N1_Group);
        }

        // PUT: api/G1_N1_Group/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutG1_N1_Group(int id, G1_N1_Group g1_N1_Group)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != g1_N1_Group.GroupNumber)
            {
                return BadRequest();
            }

            db.Entry(g1_N1_Group).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!G1_N1_GroupExists(id))
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

        // POST: api/G1_N1_Group
        [ResponseType(typeof(G1_N1_Group))]
        public IHttpActionResult PostG1_N1_Group(G1_N1_Group g1_N1_Group)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.G1_N1_Group.Add(g1_N1_Group);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (G1_N1_GroupExists(g1_N1_Group.GroupNumber))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = g1_N1_Group.GroupNumber }, g1_N1_Group);
        }

        // DELETE: api/G1_N1_Group/5
        [ResponseType(typeof(G1_N1_Group))]
        public IHttpActionResult DeleteG1_N1_Group(int id)
        {
            G1_N1_Group g1_N1_Group = db.G1_N1_Group.Find(id);
            if (g1_N1_Group == null)
            {
                return NotFound();
            }

            db.G1_N1_Group.Remove(g1_N1_Group);
            db.SaveChanges();

            return Ok(g1_N1_Group);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool G1_N1_GroupExists(int id)
        {
            return db.G1_N1_Group.Count(e => e.GroupNumber == id) > 0;
        }
    }
}