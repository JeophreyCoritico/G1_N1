using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.SqlClient;
using WebApplication1.Models;
using System.Data;

namespace ExampleWebAPI.Controllers
{
    public class TeacherController : ApiController
    {
        // GET: api/Teacher
        public IEnumerable<TeacherModel> Get()
            //public IEnumerable<TeacherModel> Get()
        {
            //return new string[] { "value1"};

            SqlConnection conn = DBConnection.GetConnection();

            SqlCommand cmd;
            SqlDataReader rdr;
            string query;
            //List<TeacherModel> output = new List<TeacherModel>();
            List<TeacherModel> output = new List<TeacherModel>();

            try
            {

                conn.Open();

                query = "select * from Teacher";
                cmd = new SqlCommand(query, conn);

                //read the data for that command
                rdr = cmd.ExecuteReader();

                //int tIDint = 0;
                //string tID = rdr.GetValue(0).ToString();
                //tIDint = int.Parse(tID);
                //tIDint = Convert.ToInt32(tID);

                while (rdr.Read())
                {
                    /*
                    output.Add(
                        "TeacherID: " + rdr.GetValue(0) +
                        ", GivenName: " + rdr.GetValue(1) + 
                        ", Surname: " + rdr.GetValue(2) + 
                        ", TeacherPassword: " + rdr.GetValue(3));

                    */


                    output.Add(new TeacherModel(Int32.Parse(rdr["TeacherID"].ToString()),
                                                rdr["GivenName"].ToString(),
                                                rdr["Surname"].ToString(),
                                                rdr["TeacherPassword"].ToString()));



                        /*
                                tIDint, 
                      int32.Parse(rdr.GetValue(0).ToString())
                                rdr.GetValue(1).ToString(),
                                rdr.GetValue(2).ToString(),
                                rdr.GetValue(3).ToString()));
                                */
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

                query = "select * from Teacher where TeacherID = " + id;
                cmd = new SqlCommand(query, conn);

                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    output =
                    "{TeacherID: " + rdr.GetValue(0) +
                    ", GivenName: \"" + rdr.GetValue(1) + "\"" +
                    ", Surname: \"" + rdr.GetValue(2) + "\"" +
                    ", TeacherPassword: \"" + rdr.GetValue(3) + "\"}";
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
        public string Post(TeacherModel MTeacher)
        {

            SqlConnection conn = DBConnection.GetConnection();

            SqlCommand cmd;
            string query;
            string output;

            try
            {

                conn.Open();

                query = "insert into Teacher(TeacherID, GivenName, Surname, TeacherPassword) values ("
                    + MTeacher.TeacherID + ", '"
                    + MTeacher.GivenName + "', '"
                    + MTeacher.Surname + "', '"
                    + MTeacher.TeacherPassword + "')";


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
        public string Put(int id, TeacherModel MTeacher)
        {

            SqlConnection conn = DBConnection.GetConnection();

            SqlCommand cmd;
            string query;
            string output;

            try
            {

                conn.Open();

                query = "update Teacher set GivenName = '" + MTeacher.GivenName +
                    "', Surname = '" + MTeacher.Surname +
                    "', TeacherPassword = '" + MTeacher.TeacherPassword +
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

        // DELETE: api/Teacher/5
        public string Delete(int id)
        {

            SqlConnection conn = DBConnection.GetConnection();

            SqlCommand cmd;
            string query;
            string output = "No teacher found";

            try
            {

                conn.Open();

                query = "Delete From Teacher where TeacherID = " + id;

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


