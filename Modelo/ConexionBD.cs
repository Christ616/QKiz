using System.Data;
using MySql.Data.MySqlClient;

namespace QKiz.Modelo
{
    public class ConexionBD
    {
        private string cadena = "server=localhost; database=bd_qkiz; user=root; Password=Kaliman66";
        public MySqlConnection conectar;
        public void AbrirConexion() 
        {
            try
            {
                conectar = new MySqlConnection();
                conectar.ConnectionString = cadena;
                conectar.Open();
                System.Diagnostics.Debug.WriteLine("Conexión Exitosa");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }
    }
}
