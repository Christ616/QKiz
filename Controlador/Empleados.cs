using Dapper;
using Crud.Models;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Swashbuckle.AspNetCore.Annotations;
using System.Security.Cryptography;

namespace Crud.Controllers
{
    [Route("api/empleados")]
    [SwaggerTag("Todo lo relacionado a los empleados")]
    [ApiController]
    public class EmpleadoController : ControllerBase
    {
        // Almacenamos una instancia de la interfaz IDb y/o declaramos un campo llamado _db de tipo privado,
        // solo lectura y con un tipo de dato IDb.
        private readonly IDb _db;

        // Inicializamos una variable de la clase Encriptacion.
        private Encriptacion _encrypt;

        // Constructor de la clase que espera que se le pase una instancia de un objeto que implemente la interfaz IDb.
        // Este parámetro se utiliza para inyectar la dependencia en la clase EmpleadoController.
        public EmpleadoController(IDb db)
        {
            _db = db;
            _encrypt = new Encriptacion(); // Creamos una nueva instancia de la clase Encriptacion.
        }

        [HttpPost]
        [SwaggerOperation(
            Summary = "Endpoint para crear un empleado",
            Description = "Requiere permisos de administrador",
            OperationId = "CrearEmpleado",
            Tags = new[] { "Empleado" }
        )]
        public async Task<IActionResult> InsertarEmpleado(Empleado nuevoEmpleado)
        {
            try
            {
                using (MySqlConnection conexion = _db.Conexion())
                {
                    // Verificamos si el modelo es válido según las reglas de validación definidas en el modelo.
                    if (ModelState.IsValid)
                    {
                        nuevoEmpleado.nombre = _encrypt.Encriptar(nuevoEmpleado.nombre); // Encriptamos el primer nombre.

                        // Creamos la SQL Query que insertará los datos a la tabla empleado.
                        var sqlInsertEmpleado = "INSERT INTO empleado (nombre, apellidop, apellidom, sexo_id, nss, rfc, telefono, email) " +
                                               "VALUES (@nombre, @apellidop, @apellidom, @sexo_id, @nss, @rfc, @telefono, @email);";

                        // Creamos la SQL Query que insertará los datos a la tabla direccion.
                        var sqlInsertDireccion = "INSERT INTO direccion (id_empleado, calle, numext, numint, cp, colonia, ciudad, estado) " +
                                              "VALUES (@id_empleado, @calle, @numext, @numint, @cp, @colonia, @ciudad, @estado);";

                        // Abrimos la conexión a la base de datos.
                        await conexion.OpenAsync();

                        // Iniciamos una transacción para asegurar la integridad de los datos.
                        using (var transaction = await conexion.BeginTransactionAsync())
                        {
                            // Insertamos en la tabla empleado.
                            await conexion.ExecuteAsync(sqlInsertEmpleado, nuevoEmpleado, transaction);

                            // Obtenemos el ID asignado automáticamente por la base de datos.
                            nuevoEmpleado.id_empleado = await conexion.ExecuteScalarAsync<uint>("SELECT LAST_INSERT_ID();", null, transaction);

                            // Insertamos en la tabla "direccion".
                            await conexion.ExecuteAsync(sqlInsertDireccion, nuevoEmpleado, transaction);

                            // Completamos la transacción.
                            await transaction.CommitAsync();
                        }

                        // Cerramos la conexión.
                        conexion.Close();

                        return Ok("Empleado insertado exitosamente.");
                    }
                    else
                    {
                        // Si el modelo no es válido, retornamos un BadRequest.
                        return BadRequest(ModelState);
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejamos cualquier excepción y retornamos un error.
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpGet]
        [SwaggerOperation(
            Summary = "Endpoint para listar los empleados",
            Description = "No se requieren permisos de administrador",
            OperationId = "ListarEmpleados",
            Tags = new[] { "Empleado" }
        )]
        public async Task<IActionResult> GetEmpleados()
        {
            try
            {
                using (MySqlConnection conexion = _db.Conexion())
                {
                    // Creamos la SQL Query que llamará a los datos a la tabla empleado y direccion mediante un INNER JOIN.
                    var sql = "SELECT empleado.id_empleado, nombre, apellidop, apellidom, sexo_id, nss, rfc, telefono, email, " +
                              " empleado.calle, empleado.numext, empleado.numint, empleado.cp, empleado.colonia, empleado.ciudad, empleado.estado" +
                              " FROM empleado INNER JOIN direccion ON empleado.id_empleado = direccion.id_empleado";

                    // Abrimos la conexión a la base de datos.
                    await conexion.OpenAsync();

                    // Ejecutamos la consulta y mapeamos los resultados a la lista de empleados.
                    var resultados = await conexion.QueryAsync<Empleado>(sql);

                    foreach (var empleado in resultados)
                    {
                        empleado.nombre = _encrypt.Desencriptar(empleado.nombre); // Por cada resultado, desencriptamos el nombre.
                    }

                    // Cerramos la conexión.
                    conexion.Close();

                    return Ok(resultados); // Retornamos un Sucess pasándole lo obtenido en la variable resultados.
                }
            }
            catch (Exception ex)
            {
                // Manejamos cualquier excepción y retornamos un error.
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        [SwaggerOperation(
            Summary = "Endpoint para actualizar un empleado",
            Description = "Requiere permisos de administrador",
            OperationId = "ActualizarEmpleado",
            Tags = new[] { "Empleado" }
        )]
        public async Task<IActionResult> ActualizarEmpleado(int id, Empleado empleadoActualizado)
        {
            try
            {
                using (MySqlConnection conexion = _db.Conexion())
                {
                    // Verificamos si el modelo es válido según las reglas de validación definidas en el modelo.
                    if (ModelState.IsValid)
                    {
                        // Creamos la SQL Query que actualizará los datos en la tabla empleado.
                        var sqlUpdateEmpleado = "UPDATE empleado SET " +
                                               "nombre = @nombre, apellidop = @apellidop, apellidom = @apellidom, " +
                                               "sexo_id = @sexo_id, nss = @nss, rfc = @rfc, telefono = @telefono, email = @email " +
                                               "WHERE id_empleado = @IDempleado;";

                        // Creamos la SQL Query que actualizará los datos en la tabla direccion.
                        var sqlUpdateDireccion = "UPDATE direccion SET " +
                                              "calle = @calle, numext = @numext, numint = @numint, cp = @cp, " +
                                              "colonia = @colonia, ciudad = @ciudad, estado = @estado " +
                                              "WHERE id_empleado = @IDempleado;";

                        // Abrimos la conexión a la base de datos.
                        await conexion.OpenAsync();

                        // Iniciamos una transacción para asegurar la integridad de los datos.
                        using (var transaction = await conexion.BeginTransactionAsync())
                        {
                            // Actualizamos en la tabla empleado.
                            await conexion.ExecuteAsync(sqlUpdateEmpleado, new
                            {
                                // Las variables en la SQL Query creada arriba(lado izquierdo) serán igual a lo recibido en el modelo(lado derecho).
                                nombre = _encrypt.Encriptar(empleadoActualizado.nombre),
                                apellidop = empleadoActualizado.apellidop,
                                apellidom = empleadoActualizado.apellidom,
                                sexo_id = empleadoActualizado.sexo_id,
                                nss = empleadoActualizado.nss,
                                rfc = empleadoActualizado.rfc,
                                telefono = empleadoActualizado.telefono,
                                email = empleadoActualizado.email,
                                IDempleado = id
                            }, transaction);

                            // Actualizamos en la tabla direccion.
                            await conexion.ExecuteAsync(sqlUpdateDireccion, new
                            {
                                // Las variables en la SQL Query creada arriba(lado izquierdo) serán igual a lo recibido en el modelo(lado derecho).
                                calle = empleadoActualizado.calle,
                                numext = empleadoActualizado.numext,
                                numint = empleadoActualizado.numint,
                                cp = empleadoActualizado.cp,
                                colonia = empleadoActualizado.colonia,
                                ciudad = empleadoActualizado.ciudad,
                                estado = empleadoActualizado.estado,
                                IDempleado = id
                            }, transaction);

                            // Completamos la transacción.
                            await transaction.CommitAsync();
                        }

                        // Cerramos la conexión.
                        conexion.Close();

                        return Ok("Empleado actualizado exitosamente."); // Retornamos un Success pasándole una string.
                    }
                    else
                    {
                        // Si el modelo no es válido, retornamos un BadRequest.
                        return BadRequest(ModelState);
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejamos cualquier excepción y retornar un error.
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpDelete("{id:int}")]
        [SwaggerOperation(
            Summary = "Endpoint para eliminar un empleado",
            Description = "Requiere permisos de administrador",
            OperationId = "EliminarEmpleado",
            Tags = new[] { "Empleado" }
        )]
        public async Task<IActionResult> DeleteEmpleado(int id)
        {
            try
            {
                using (MySqlConnection conexion = _db.Conexion())
                {
                    // Inicia una transacción para asegurar la integridad de los datos.
                    await conexion.OpenAsync();
                    using (var transaction = await conexion.BeginTransactionAsync())
                    {
                        // Eliminamos al empleado de la tabla direccion.
                        var sqlDeleteDireccion = "DELETE FROM direccion WHERE id_empleado = @IDempleado";
                        await conexion.ExecuteAsync(sqlDeleteDireccion, new { IDempleado = id }, transaction);

                        // Elimina a la empleado de la tabla empleado.
                        // En la base de datos, la relación de direccion a empleado tiene ON DELETE CASCADE, por lo que esto puede no ser necesario.
                        var sqlDeleteEmpleado = "DELETE FROM empleado WHERE id_empleado = @IDempleado";
                        // Realizamos la eliminación de manera asíncrona usando Dapper.
                        await conexion.ExecuteAsync(sqlDeleteEmpleado, new { IDempleado = id }, transaction);

                        // await Indica que la ejecución del método espera de forma asincrónica hasta que la tarea se complete.
                        // conexion: El objeto de conexión a la base de datos.
                        // ExecuteAsync: Método proporcionado por Dapper que ejecuta una consulta SQL o un procedimiento almacenado en la base de datos y devuelve el número de filas afectadas.

                        // sqlDeleteEmpleado contiene la instrucción SQL para eliminar registros de la tabla "empleado" en la base de datos.
                        // new { IDempleado = id }: Esto crea un objeto anónimo en C# con una propiedad llamada IDempleado y un valor asociado id.
                        // Dapper utiliza este objeto para asignar parámetros en la consulta SQL de manera segura para evitar la inyección de SQL.
                        // transaction: se utiliza para agrupar operaciones de base de datos en una transacción. El método ExecuteAsync se ejecutará dentro de esta transacción.

                        // Completamos la transacción.
                        await transaction.CommitAsync();
                    }
                }

                return NoContent(); // Retornamos un NoContent. O un 204 pero sin datos que devolver.
            }
            catch (Exception ex)
            {
                // Manejamos cualquier excepción y retornamos un error.
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }
    }
}
