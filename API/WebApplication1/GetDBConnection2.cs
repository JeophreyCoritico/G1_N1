using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

namespace ExampleWebAPI
{
    public static class DBConnection
    {
        public static SqlConnection GetConnection()
        {
            //@"Server=myServerAddress;Database=myDatabase;User Id=MyUsername;Password=myPassword;"
            string ConnString = @"Server = dadtj.database.windows.net;Database = DADExample;User Id = dbstudent;Password = abc1234!;";
            return new SqlConnection(ConnString);
        }
    }
}