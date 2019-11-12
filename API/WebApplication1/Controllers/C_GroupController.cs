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
using ExampleWebAPI;

namespace WebApplication1.Controllers
{
    public class C_GroupController : ApiController
    {
        private DADExampleEntities db = new DADExampleEntities();

        // GET: api/C_Group
        public IEnumerable<C_Group> GetC_Group()
        {
            SqlConnection conn = DBConnection.GetConnection();

            SqlCommand cmd;
            SqlDataReader rdr;
            string query;
            List<C_Group> output = new List<C_Group>();

            try
            {

                conn.Open();

                query = "select * from _Group";
                cmd = new SqlCommand(query, conn);

                //read the data for that command
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    output.Add(new C_Group(Int32.Parse(rdr["GroupNumber"].ToString())));
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
            }
            return output;
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
        public string PostC_Group(C_Group MGroup)
        {

            SqlConnection conn = DBConnection.GetConnection();

            SqlCommand cmd;
            string query;
            string output;

            try
            {

                conn.Open();

                query = "insert into _Group(GroupNumber) values ("
                    + MGroup.GroupNumber + ")";


                cmd = new SqlCommand(query, conn);

                //read the data for that command
                output = cmd.ExecuteNonQuery().ToString() + " Rows Inserted";

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

            return output;
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