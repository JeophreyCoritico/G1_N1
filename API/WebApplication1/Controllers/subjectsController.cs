using ExampleWebAPI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class subjectsController : ApiController
    {
        private DADExampleEntities db = new DADExampleEntities();

        // GET: api/subjects
        public IEnumerable<subject> Getsubjects()
        {
            SqlConnection conn = DBConnection.GetConnection();

            SqlCommand cmd;
            SqlDataReader rdr;
            string query;
            List<subject> output = new List<subject>();

            try
            {

                conn.Open();

                query = "select * from subject";
                cmd = new SqlCommand(query, conn);

                //read the data for that command
                rdr = cmd.ExecuteReader();


                while (rdr.Read())
                {
                    output.Add(new subject(rdr["SubjectCode"].ToString(),
                                           rdr["Description"].ToString()));
                                                
                }

            }
            catch (Exception e)
            {
                throw e;

                //throw e;
            }
            finally
            {
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
            }
            return output;
        }

        // GET: api/subjects/5
        [ResponseType(typeof(subject))]
        public IHttpActionResult Getsubject(string id)
        {
            subject subject = db.subjects.Find(id);
            if (subject == null)
            {
                return NotFound();
            }

            return Ok(subject);
        }

        // PUT: api/subjects/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putsubject(string id, subject subject)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != subject.SubjectCode)
            {
                return BadRequest();
            }

            db.Entry(subject).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!subjectExists(id))
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

        // POST: api/subjects
        [ResponseType(typeof(subject))]
        public string Postsubject(subject subject)
        {
            SqlConnection conn = DBConnection.GetConnection();

            SqlCommand cmd;
            string query;
            string output;

            try
            {

                conn.Open();

                query = "insert into subject(SubjectCode, Description) values ('"
                    + subject.SubjectCode + "', '"
                    + subject.Description + "')";



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

        // DELETE: api/subjects/5
        [ResponseType(typeof(subject))]
        public IHttpActionResult Deletesubject(string id)
        {
            subject subject = db.subjects.Find(id);
            if (subject == null)
            {
                return NotFound();
            }

            db.subjects.Remove(subject);
            db.SaveChanges();

            return Ok(subject);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool subjectExists(string id)
        {
            return db.subjects.Count(e => e.SubjectCode == id) > 0;
        }
    }
}