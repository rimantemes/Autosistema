using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Auto_sistema
{
    /// <summary>
    /// Class adds expenses to database
    /// </summary>
    class expenses
    {
     //   private string kuras;
        private string kitos;
        private string remontas;

    public expenses(string kitos, string remontas)
        {
            //this.kuras = kuras;
            this.kitos = kitos;
            this.remontas = remontas;
        }

    public void add_expenses(int id)
        {
            var connection = System.Configuration.ConfigurationManager.ConnectionStrings["ServerAttach"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();
                string cmd = "UPDATE [Lenta] SET Remonto_islaidos=@Remonto_islaidos, Kitos_islaidos=@Kitos_islaidos WHERE Id=@autoid";
                
                using (SqlCommand at = new SqlCommand(cmd, conn))
                {
                    at.Parameters.AddWithValue("@Remonto_islaidos", remontas);
                    at.Parameters.AddWithValue("@Kitos_islaidos", kitos);
                    at.Parameters.AddWithValue("@autoid", id);

                    at.ExecuteNonQuery();
                }
                // conn.Close();
            }
        }
    }
}
