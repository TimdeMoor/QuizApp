using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace QuizLib.Data
{
    public static class Db
    {
        public static SqlConnection conn =
            new SqlConnection(ConfigurationManager.ConnectionStrings["DataConnectionString"].ConnectionString);

        //Opens connection to the database
        public static void TryOpenConnection()
        {
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
        }

        //Closes connection to the database
        public static void TryCloseConnection()
        {
            if (conn.State != ConnectionState.Closed)
            {
                conn.Close();
            }
        }
    }
}
