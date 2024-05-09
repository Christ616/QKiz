using MySql.Data.MySqlClient;

namespace Crud.Models
{
    public interface IDb
    {
        MySqlConnection Conexion(); // Definimos el método Conexion().
    }
}
