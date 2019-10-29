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
            string ConnString = @"Server = swin.database.windows.net;Database = DAD;User Id = DAD;Password = R@ndom!1;";
            return new SqlConnection(ConnString);
        }
    }
}