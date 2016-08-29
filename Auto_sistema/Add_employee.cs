using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Auto_sistema
{
/// <summary>
/// Class adds employees to database
/// </summary>
    class Add_employee
    {
        private string vardas;
        private string pavarde;

        public Add_employee(string vardas, string pavarde)
        {
            this.vardas = vardas;
            this.pavarde = pavarde;   
        }

        public void saveAuto()
        {
            var connection = System.Configuration.ConfigurationManager.ConnectionStrings["ServerAttach"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();
                string cmd = "INSERT INTO [Darbuotojai] (Vardas, Pavarde) " +
                                        "Values (@Vardas, @Pavarde)";
                using (SqlCommand at = new SqlCommand(cmd, conn))
                {
                    at.Parameters.AddWithValue("@Vardas", vardas);
                    at.Parameters.AddWithValue("@Pavarde", pavarde);

                    at.ExecuteNonQuery();
                }
                // conn.Close();
            }
        }
    }
}
