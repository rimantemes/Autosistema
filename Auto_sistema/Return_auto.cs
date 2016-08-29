using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Auto_sistema
{
    /// <summary>
    /// Class adds returned auto data to database
    /// </summary>
    class Return_auto
    {
        private DateTime glaikas;
        string ridaPo;
        private int kt1;
        string ridaPries;
        string kuras;
        int idToUpdate;

        public Return_auto(DateTime glaikas, string ridaPo, int kt1, string ridaPries, string kuras, int idToUpdate)
        {
            this.glaikas = glaikas;
            this.ridaPo = ridaPo;
            this.kt1 = kt1;
            this.ridaPries = ridaPries;
            this.kuras = kuras;
            this.idToUpdate = idToUpdate;
        }

        public void g_laikas()
        {
            var connection = System.Configuration.ConfigurationManager.ConnectionStrings["ServerAttach"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();
                string cmd = "UPDATE [Lenta] SET Automobilio_grazinimo_data=@Automobilio_grazinimo_data, Automobilio_rida_po=@Automobilio_rida_po, K_t=@K_t, Automobilio_rida_pries=@updateRidaPries, Kuro_islaidos=@Kuro_islaidos WHERE Id=@autoid";
                using (SqlCommand at = new SqlCommand(cmd, conn))
                {
                    at.Parameters.AddWithValue("@Automobilio_grazinimo_data", glaikas);
                    at.Parameters.AddWithValue("@Automobilio_rida_po", ridaPo);
                    at.Parameters.AddWithValue("@autoid", idToUpdate);
                    at.Parameters.AddWithValue("@K_t", kt1);
                    at.Parameters.AddWithValue("@updateRidaPries", ridaPries);
                    at.Parameters.AddWithValue("@Kuro_islaidos", kuras);

                    at.ExecuteNonQuery();
                }
                // conn.Close();
            }
        }
    }
}
