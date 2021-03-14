using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace TBashaBari.Controllers
{
    public class DatabaseConnection
    {
        SqlCommand queryString = new SqlCommand();
        SqlDataReader datareader;
        SqlConnection conn = new SqlConnection();
        public void DbConnect()
        {
            /*
              Actual string looks like this: 
                             Dinar :-  "Data Source = \"DESKTOP-RUB62SE\\SQLEXPRESS\"; database = \"BashaBariWeb\"; integrated security = SSPI;";
                             saky  :-  "Data Source = \"DESKTOP-37APNEN\\SQLEXPRESS\"; database = \"BashaBariWeb\"; integrated security = SSPI;";
                             Sajid :-  "Data Source = \"DESKTOP- Sajid tr server name de \\SQLEXPRESS\"; database = \"BashaBariWeb\"; integrated security = SSPI;";
             */
            conn.ConnectionString = "Data Source = \"DESKTOP-37APNEN\\SQLEXPRESS\"; database = \"BashaBariWeb\"; integrated security = SSPI;";
            conn.Open();
            queryString.Connection = conn;

        }

        public SqlDataReader ExeQuery(string queryString)
        {
            try
            {
                this.queryString.CommandText = queryString;
                datareader = this.queryString.ExecuteReader();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return datareader;
        }

        public void CloseDbConnect()
        {
            conn.Close();
        }
    }
}
