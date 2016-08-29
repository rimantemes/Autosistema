using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;


namespace Auto_sistema
{
    /// <summary>
    /// Class adds rented auto data to database
    /// </summary>
    class Rent_auto
    {
        private DateTime plaikas;
        private int autoId;
        private int darbId;
        private int kt;
       // private int ridos;
        
        public Rent_auto(DateTime plaikas, int autoId, int darbId, int kt)
        {
            this.plaikas = plaikas;
            this.autoId = autoId;
            this.darbId = darbId;
            this.kt = kt;
          //  this.ridos = ridos;
            
        }
        public void updateAutoRow()
        {
            var connection = System.Configuration.ConfigurationManager.ConnectionStrings["ServerAttach"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();
                string cmd = "UPDATE [Lenta] SET Automobilio_grazinimo_data=NULL, Automobilio_paemimo_data=@Automobilio_paemimo_data, AsmuoId_FK=@darbId, K_t=@K_t WHERE Id=@autoId";
                using (SqlCommand at = new SqlCommand(cmd, conn))
                {
                    at.Parameters.AddWithValue("@darbId", darbId);
                    at.Parameters.AddWithValue("@autoId", autoId);
                    at.Parameters.AddWithValue("@Automobilio_paemimo_data", plaikas);
                    at.Parameters.AddWithValue("@K_t", kt);
                    //      at.Parameters.AddWithValue("@valueToUpdate", ridos);
                    //Automobilio_rida_pries = @valueToUpdate

                    at.ExecuteNonQuery();
                }
            }
        }
    }
}
