using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace ApartmentLib
{
    class DBConnection
    {
        public bool Open(SqlConnection conn)
        {
            try
            {
                conn.ConnectionString = "Data Source=LAPTOP-QDSH6007\\SQLEXPRESS; Initial Catalog = RentApartment; Integrated Security = True;";
                conn.Open();
                return true;
            }
            catch (System.Data.SqlClient.SqlException)
            {
                return false;
            }
        }
    }
}
