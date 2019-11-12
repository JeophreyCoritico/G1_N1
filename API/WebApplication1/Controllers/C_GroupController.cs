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
using WebApplication1.Models;
using System.Data.SqlClient;

namespace WebApplication1.Controllers
{
    public class C_GroupController : ApiController
    {
        private DADExampleEntities db = new DADExampleEntities();

        // GET: api/C_Group
        public IQueryable<C_Group> GetC_Group()
        {
            SqlConnection conn = DBConnection.GetConnection();

            SqlCommand cmd;
            SqlDataReader rdr;
            string query;
            List<TeacherModel> output = new List<TeacherModel>();

            return db.C_Group;
        }

        // GET: api/C_Group/5
        [ResponseType(typeof(C_Group))]
        public IHttpActionResult GetC_Group(int id)
        {
            C_Group c_Group = db.C_Group.Find(id);
            if (c_Group == null)
            {
                return NotFound();
            }

            return Ok(c_Group);
        }

        // PUT: api/C_Group/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutC_Group(int id, C_Group c_Group)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != c_Group.GroupNumber)
            {
                return BadRequest();
            }

            db.Entry(c_Group).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!C_GroupExists(id))
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

        // POST: api/C_Group
        [ResponseType(typeof(C_Group))]
        public IHttpActionResult PostC_Group(C_Group c_Group)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.C_Group.Add(c_Group);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (C_GroupExists(c_Group.GroupNumber))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = c_Group.GroupNumber }, c_Group);
        }

        // DELETE: api/C_Group/5
        [ResponseType(typeof(C_Group))]
        public IHttpActionResult DeleteC_Group(int id)
        {
            C_Group c_Group = db.C_Group.Find(id);
            if (c_Group == null)
            {
                return NotFound();
            }

            db.C_Group.Remove(c_Group);
            db.SaveChanges();

            return Ok(c_Group);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool C_GroupExists(int id)
        {
            return db.C_Group.Count(e => e.GroupNumber == id) > 0;
        }
    }
}