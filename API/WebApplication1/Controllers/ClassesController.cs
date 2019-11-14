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
    public class ClassesController : ApiController
    {
        

        // GET: api/Classes
        public IEnumerable<Class> Get()
        {
            SqlConnection conn = DBConnection.GetConnection();

            SqlCommand cmd;
            SqlDataReader rdr;
            string query;
            List<Class> output = new List<Class>();
            try
            {

                conn.Open();

                query = "select * from Class";
                cmd = new SqlCommand(query, conn);

                //read the data for that command
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    output.Add(new  Class(Int32.Parse(rdr["TeacherID"].ToString()),
                                                Int32.Parse(rdr["GroupNumber"].ToString()),
                                                rdr["SubjectCode"].ToString(),
                                                rdr["RoomNo"].ToString(),
                                                rdr["Day"].ToString(),
                                                rdr["Description"].ToString(),
                                                TimeSpan.Parse(rdr["StartTime"].ToString()),
                                                TimeSpan.Parse(rdr["EndTime"].ToString()),
                                                Int32.Parse(rdr["Capacity"].ToString())));
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
        public string Put(int id,  Class MClass)
        {

            SqlConnection conn = DBConnection.GetConnection();

            SqlCommand cmd;
            string query;
            string output;

            try
            {

                conn.Open();

                query = "update Class set GroupNumber = '" + MClass.GroupNumber +
                  "', SubjectCode = '" + MClass.SubjectCode +
                  "', RoomNo = '" + MClass.RoomNo +
                  "', Day = '" + MClass.Day +
                  "', Description = '" +MClass.Description +
                  "', StartTime = '" + MClass.StartTime +
                  "', EndTime = '" + MClass.EndTime +
                  "', Capacity = '" + MClass.Capacity +
                  "' where TeacherID = " + id;


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

        // POST: api/Classes
        public string Post(Class MClass)
        {

            SqlConnection conn = DBConnection.GetConnection();

            SqlCommand cmd;
            string query;
            string output;

            try
            {

                conn.Open();

                query = "insert into Class(TeacherID, GroupNumber, SubjectCode, RoomNo, Day, Description, StartTime, EndTime, Capacity) values ("
                  + MClass.TeacherID + ", '"
                  + MClass.GroupNumber + "', '"
                  + MClass.SubjectCode + "', '"
                  + MClass.RoomNo + "', '"
                   + MClass.Day + "', '"
                   + MClass.Description + "', '"
                    + MClass.StartTime + "', '"
                    + MClass.EndTime + "', '"
                  + MClass.Capacity + "')";


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

        // DELETE: api/Classes/5
        public string Delete(int id)
        {

            SqlConnection conn = DBConnection.GetConnection();

            SqlCommand cmd;
            string query;
            string output = "No Class found";

            try
            {

                conn.Open();

                query = "Delete From Class where TeacherID = " + id;

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