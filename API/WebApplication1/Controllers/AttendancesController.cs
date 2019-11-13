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
    public class   AttendancesController : ApiController
    {
        // GET: api/Teacher
        public IEnumerable<Attendance> Get()
        {

            SqlConnection conn = DBConnection.GetConnection();

            SqlCommand cmd;
            SqlDataReader rdr;
            string query;
            List<Attendance> output = new List<Student>();

            try
            {

                conn.Open();

                query = "select * from Attendance";
                cmd = new SqlCommand(query, conn);

                //read the data for that command
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    output.Add(new Attendance(
                                              //rdr["SignIn"].ToString(),
                                              //rdr["SignOut"].ToString(), 
                                              rdr["SignIn"].ToString(),
                                              rdr["SignOut"].ToString(),
                                              Int32.Parse(rdr["TeacherID"].ToString()),
                                                Int32.Parse(rdr["GroupNumber"].ToString()),
                                                rdr["SubjectCode"].ToString(),
                                                rdr["RoomNo"].ToString(),
                                                Int32.Parse(rdr["Barcode"].ToString(),
                                                Convert.ToBoolean(rdr["EarlyLeave"].ToString()),
                                                rdr["Late"].ToString()));

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

                query = "select * from Attendance where StudentID = " + id;
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
    public class AttendancesController : ApiController
    {
        private DADExampleEntities db = new DADExampleEntities();

        // GET: api/Attendances
        public IQueryable<Attendance> GetAttendances()
        {
            return db.Attendances;
        }

        // GET: api/Attendances/5
        [ResponseType(typeof(Attendance))]
        public IHttpActionResult GetAttendance(DateTime id)
        {
            Attendance attendance = db.Attendances.Find(id);
            if (attendance == null)
            {
                return NotFound();
            }

            return Ok(attendance);
        }

        // PUT: api/Attendances/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAttendance(DateTime id, Attendance attendance)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != attendance.SignIn)
            {
                return BadRequest();
            }

            db.Entry(attendance).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AttendanceExists(id))
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

        // POST: api/Attendances
        [ResponseType(typeof(Attendance))]
        public IHttpActionResult PostAttendance(Attendance attendance)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Attendances.Add(attendance);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (AttendanceExists(attendance.SignIn))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = attendance.SignIn }, attendance);
        }

        // DELETE: api/Attendances/5
        [ResponseType(typeof(Attendance))]
        public IHttpActionResult DeleteAttendance(DateTime id)
        {
            Attendance attendance = db.Attendances.Find(id);
            if (attendance == null)
            {
                return NotFound();
            }

            db.Attendances.Remove(attendance);
            db.SaveChanges();

            return Ok(attendance);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AttendanceExists(DateTime id)
        {
            return db.Attendances.Count(e => e.SignIn == id) > 0;
        }
    }
}*/
