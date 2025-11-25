using System.Configuration;
using MySql.Data.MySqlClient;

namespace FerroApp.App_Code
{
    public static class ConexionBD
    {
        public static MySqlConnection ObtenerConexion()
        {
            var cadena = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;
            return new MySqlConnection(cadena);
        }

        public static MySqlCommand CrearComando(string consulta, MySqlConnection conexion)
        {
            var comando = new MySqlCommand(consulta, conexion)
            {
                CommandType = System.Data.CommandType.Text
            };
            return comando;
        }
    }
}
