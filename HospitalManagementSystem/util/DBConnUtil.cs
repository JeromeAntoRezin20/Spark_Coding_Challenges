using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace HospitalManagementSystem.util
{
    public class DBConnUtil
    {
        private static readonly string connectionString = "Server=MSI;Database=HospitalManagementSystem;Integrated Security=True;TrustServerCertificate=True";

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}
