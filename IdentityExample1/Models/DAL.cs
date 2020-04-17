using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;

namespace IdentityExample1.Models
{
    public class DAL
    {
        private SqlConnection conn;

        public DAL(string connectionString)
        {
            conn = new SqlConnection(connectionString);
        }

        public int CreateTask(UserTask t)
        {
            string sqlQuery = "INSERT INTO Tasks (OwnerId, Description, DueDate, Completed) ";
            sqlQuery += "VALUES (@OwnerId, @Description, @DueDate, @Completed)";

            return conn.Execute(sqlQuery);
        }
    }
}