using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace DataAccessLayer
{
    internal static class DBConnection
    {
        // readonly for static, const for concrete classes
        // for connectionString, r-click on the data connection, properties, copy and past connection string, knowing this connection string is important 
        // @ makes the string a literal
        private static readonly string connectionString = @"Data Source=localhost;Initial Catalog=MillennialResort_DB;Integrated Security=True";

        // This is the only place in your app that 
        // the database connection string should be
        // found and the only code that should create 
        // databse connections.  You only want 1 way to connect to the DB

        public static SqlConnection GetDbConnection()
        {
            // a connection needs a connection string
            var conn = new SqlConnection(connectionString);
            return conn;
        }
    }
}
