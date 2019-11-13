using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.SqlClient;
using WebApplication1.Models;
using System.Data;
using ExampleWebAPI;

namespace WebApplication1.Controllers
{
    public class StudentsController : ApiController
    {
        // GET: api/Teacher
        public IEnumerable<Student> Get()
        {

            SqlConnection conn = DBConnection.GetConnection();

            SqlCommand cmd;
            SqlDataReader rdr;
            string query;
            List<Student> output = new List<Student>();

            try
            {

                conn.Open();

                query = "select * from Student";
                cmd = new SqlCommand(query, conn);

                //read the data for that command
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    output.Add(new Student(Int32.Parse(rdr["Barcode"].ToString()),
                                                Int32.Parse(rdr["GroupNumber"].ToString()),
                                                rdr["GivenName"].ToString(),
                                                rdr["Surname"].ToString(),
                                                Int32.Parse(rdr["StudentID"].ToString())));
                                                  
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

        // GET: api/Teacher/5
        public string Get(int id)
        {
            SqlConnection conn = DBConnection.GetConnection();

            SqlCommand cmd;
            SqlDataReader rdr;
            string query;
            string output = "no record found";


            try
            {
                conn.Open();

                query = "select * from Student where StudentID = " + id;
                cmd = new SqlCommand(query, conn);

                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    output =
                    "{Barcode: " + rdr.GetValue(0) +
                    ", GroupNumber: \"" + rdr.GetValue(1) + "\"" +
                    ", GivenName: \"" + rdr.GetValue(2) + "\"" +
                    ",  Surname: \"" + rdr.GetValue(3) + "\"}" +
                    ", StudentID: \"" + rdr.GetValue(4) + "\"}";
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
        // POST: api/Teacher
        public string Post(Student MStudent)
        {

            SqlConnection conn = DBConnection.GetConnection();

            SqlCommand cmd;
            string query;
            string output;

            try
            {

                conn.Open();

                query = "insert into Student(Barcode, GroupNumber, GivenName, Surname, StudentID) values ("
                    + MStudent.Barcode + ", '"
                    + MStudent.GroupNumber + "', '"
                    + MStudent.GivenName + "', '"
                     + MStudent.Surname + "', '"
                    + MStudent.StudentID + "')";


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

        // PUT: api/Teacher/5
        public string Put(int id, Student MStudent)
        {

            SqlConnection conn = DBConnection.GetConnection();

            SqlCommand cmd;
            string query;
            string output;

            try
            {

                conn.Open();

                query = "update Student set Barcode = '" + MStudent.Barcode +
                    "', GroupNumber = '" + MStudent.GroupNumber +
                    "', GivenName = '" + MStudent.GivenName +
                    "', Surname = '" + MStudent.Surname +
                    "' where StudentID = " + id;


                cmd = new SqlCommand(query, conn);

                //read the data for that command
                output = cmd.ExecuteNonQuery().ToString() + " Rows updated";

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

        // DELETE: api/Teacher/5
        public string Delete(int id)
        {

            SqlConnection conn = DBConnection.GetConnection();

            SqlCommand cmd;
            string query;
            string output = "No student found";

            try
            {

                conn.Open();

                query = "Delete From Student where StudentID = " + id;

                cmd = new SqlCommand(query, conn);

                //read the data for that command
                output = cmd.ExecuteNonQuery().ToString() + " Row Deleted";

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
    }
}








/*using System;
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
    public class StudentsController : ApiController
    {
        private DADExampleEntities db = new DADExampleEntities();

        // GET: api/Students
        public IQueryable<Student> GetStudents()
        {
            return db.Students;
        }

        // GET: api/Students/5
        [ResponseType(typeof(Student))]
        public IHttpActionResult GetStudent(int id)
        {
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return NotFound();
            }

            return Ok(student);
        }

        // PUT: api/Students/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutStudent(int id, Student student)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != student.Barcode)
            {
                return BadRequest();
            }

            db.Entry(student).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(id))
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

        // POST: api/Students
        [ResponseType(typeof(Student))]
        public IHttpActionResult PostStudent(Student student)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Students.Add(student);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (StudentExists(student.Barcode))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = student.Barcode }, student);
        }

        // DELETE: api/Students/5
        [ResponseType(typeof(Student))]
        public IHttpActionResult DeleteStudent(int id)
        {
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return NotFound();
            }

            db.Students.Remove(student);
            db.SaveChanges();

            return Ok(student);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool StudentExists(int id)
        {
            return db.Students.Count(e => e.Barcode == id) > 0;
        }
    }
}*/
