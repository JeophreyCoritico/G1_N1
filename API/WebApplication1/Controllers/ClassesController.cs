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

namespace WebApplication1.Controllers
{
    public class ClassesController : ApiController
    {
        private DADExampleEntities db = new DADExampleEntities();

        // GET: api/Classes
        public IQueryable<Class> GetClasses()
        {
            return db.Classes;
        }

        //HELLOOOOOOOOOOOOOOOOOOOOOOOOOO

        // GET: api/Classes/5
        //[ResponseType(typeof(Class))]
        //public IHttpActionResult GetClass(int id)
        //{
        //    Class @class = db.Classes.Find(id);
        //    if (@class == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(@class);
        //}

        public string GetClass(int id)
        {
            SqlConnection conn = DBConnection.GetConnection();

            SqlCommand cmd;
            SqlDataReader rdr;
            string query;
            string output = "no record found";

            try
            {
                conn.Open();

                query = "select * from Class where TeacherID = " + id;
                cmd = new SqlCommand(query, conn);

                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    output =
                    "{TeacherID: " + rdr.GetValue(0) +
                    ", GroupNumber: \"" + rdr.GetValue(1) + "\"" +
                    ", SubjectCode: \"" + rdr.GetValue(2) + "\"" +
                    ", RoomNo: \"" + rdr.GetValue(3) + "\"" +
                    ", Day: \"" + rdr.GetValue(4) + "\"" +
                    ", Description: \"" + rdr.GetValue(5) + "\"" +
                    ", StartTime: \"" + rdr.GetValue(6) + "\"" +
                    ", EndTime: \"" + rdr.GetValue(7) + "\"" +
                    ", Capacity: \"" + rdr.GetValue(8) + "\"}";
                }
            }
            catch (Exception e)
            {
                output = e.Message;
            }
            finally
            {
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
            }
            conn.Close();

            return output;
        }


        // PUT: api/Classes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutClass(int id, Class @class)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != @class.TeacherID)
            {
                return BadRequest();
            }

            db.Entry(@class).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClassExists(id))
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

        // POST: api/Classes
        [ResponseType(typeof(Class))]
        public IHttpActionResult PostClass(Class @class)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Classes.Add(@class);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ClassExists(@class.TeacherID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = @class.TeacherID }, @class);
        }

        // DELETE: api/Classes/5
        [ResponseType(typeof(Class))]
        public IHttpActionResult DeleteClass(int id)
        {
            Class @class = db.Classes.Find(id);
            if (@class == null)
            {
                return NotFound();
            }

            db.Classes.Remove(@class);
            db.SaveChanges();

            return Ok(@class);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ClassExists(int id)
        {
            return db.Classes.Count(e => e.TeacherID == id) > 0;
        }
    }
}