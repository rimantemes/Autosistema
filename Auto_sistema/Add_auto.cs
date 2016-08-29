using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace Auto_sistema
{
    /// <summary>
    /// Class adds auto data to database
    /// </summary>
    class Add_auto
    {
        private string numeris;
        private string marke;
        private DateTime tlaikas;
        private DateTime dlaikas;
        private string rida;
        private int kt2;

        public Add_auto(string numeris, string marke, DateTime tlaikas, DateTime dlaikas, string rida, int kt2)
        {
            this.numeris = numeris;
            this.marke = marke;
            this.tlaikas = tlaikas;
            this.dlaikas = dlaikas;
            this.rida = rida;
            this.kt2 = kt2;
        }

        public void saveAuto()
        {
            var connection = System.Configuration.ConfigurationManager.ConnectionStrings["ServerAttach"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();
                string cmd = "INSERT INTO [Lenta] (Valstybinis_Nr, Automobilio_marke, Tech_apziuros_galiojimo_laikas, Draudimo_galiojimo_laikas, Automobilio_rida_pries, K_t) "+
                                        "Values (@Valstybinis_Nr, @Automobilio_marke, @Tech_apziuros_galiojimo_laikas, @Draudimo_galiojimo_laikas, @Automobilio_rida_pries, @K_t)";
                using (SqlCommand at = new SqlCommand(cmd, conn))
                {
                    at.Parameters.AddWithValue("@Valstybinis_Nr", numeris);
                    at.Parameters.AddWithValue("@Automobilio_marke", marke);
                    at.Parameters.AddWithValue("@Tech_apziuros_galiojimo_laikas", tlaikas);
                    at.Parameters.AddWithValue("@Draudimo_galiojimo_laikas", dlaikas);
                    at.Parameters.AddWithValue("@Automobilio_rida_pries", rida);
                    at.Parameters.AddWithValue("@K_t", kt2);

                    at.ExecuteNonQuery();
                }
               // conn.Close();
            }
        }
    }
}
