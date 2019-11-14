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
    public class AttendancesController : ApiController
    {
        // GET: api/Attendance
        public IEnumerable<Attendance> Get()
        {

            SqlConnection conn = DBConnection.GetConnection();

            SqlCommand cmd;
            SqlDataReader rdr;
            string query;
            List<Attendance> output = new List<Attendance>();

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
                                 DateTime.Parse(rdr["SignIn"].ToString()),
                                 DateTime.Parse(rdr["SignOut"].ToString()),
                                 Int32.Parse(rdr["TeacherID"].ToString()),
                                  Int32.Parse(rdr["GroupNumber"].ToString()),
                                  rdr["SubjectCode"].ToString(),
                                  rdr["RoomNo"].ToString(),
                                  Int32.Parse(rdr["Barcode"].ToString()),
                                  Convert.ToBoolean(rdr["EarlyLeave"].ToString()),
                                  Convert.ToBoolean(rdr["EarlyLeave"].ToString())));

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

        // GET: api/Attendance/5
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
                    "{SignIn: " + rdr.GetValue(0) +
                    ", SignOut: \"" + rdr.GetValue(1) + "\"" +
                    ", TeacherID: \"" + rdr.GetValue(2) + "\"" +
                    ",  GroupNumber: \"" + rdr.GetValue(3) + "\"}" +
                    ",  SubjectCode: \"" + rdr.GetValue(4) + "\"}" +
                    ",  RoomNo: \"" + rdr.GetValue(5) + "\"}" +
                    ",  Barcode: \"" + rdr.GetValue(6) + "\"}" +
                    ",  EarlyLeave: \"" + rdr.GetValue(7) + "\"}" +
                    ", Late: \"" + rdr.GetValue(8) + "\"}";
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
        // POST: api/Attendance
        public string Post(Attendance attendance)
        {

            SqlConnection conn = DBConnection.GetConnection();

            SqlCommand cmd;
            string query;
            string output;

            try
            {

                conn.Open();

                query = "insert into Attendance(SignIn, SignOut, TeacherID, GroupNumber, SubjectCode, RoomNo, Barcode, EarlyLeave, Late ) values ("
                  + attendance.SignIn + ", '"
                  + attendance.SignOut + "', '"
                  + attendance.TeacherID + "', '"
                  + attendance.GroupNumber + "', '"
                   + attendance.SubjectCode + "', '"
                   + attendance.RoomNo + "', '"
                    + attendance.Barcode + "', '"
                    + attendance.EarlyLeave + "', '"
                  + attendance.Late + "')";


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

        // PUT: api/Attendance/5
        public string Put(int id, Attendance attendance)
        {

            SqlConnection conn = DBConnection.GetConnection();

            SqlCommand cmd;
            string query;
            string output;

            try
            {

                conn.Open();

                query = "update Attendance set SignIn = '" + attendance.SignIn +
                  "', SignOut = '" + attendance.SignOut +
                  "', TeacherID = '" + attendance.TeacherID +
                  "', GroupNumber = '" + attendance.GroupNumber +
                  "', SubjectCode = '" + attendance.SubjectCode +
                  "', RoomNo = '" + attendance.RoomNo +
                  "', EarlyLeave = '" + attendance.EarlyLeave +
                  "', Late = '" + attendance.Late +
                  "' where Barcode = " + id;


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

        // DELETE: api/Attendance/5
        public string Delete(int id)
        {

            SqlConnection conn = DBConnection.GetConnection();

            SqlCommand cmd;
            string query;
            string output = "No Attendance found";

            try
            {

                conn.Open();

                query = "Delete From Attendance where Barcode = " + id;

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
