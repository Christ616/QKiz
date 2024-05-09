using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using QKiz.Models;

namespace QKiz.Pages
{
    public class EmpleadosModel : PageModel
    {
        private readonly BdQkizContext _context;
        private readonly ILogger<EmpleadosModel> _logger;

        public EmpleadosModel(BdQkizContext context, ILogger<EmpleadosModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        [BindProperty]
        public Empleado Empleado { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                // Si el modelo no es válido, regresa a la página para mostrar errores
                return Page();
            }

            try
            {
                // Crea un nuevo objeto Empleado y asigna los valores del formulario
                var nuevoEmpleado = new Empleado
                {
                    Nombre = Empleado.Nombre,
                    Apellidop = Empleado.Apellidop,
                    Apellidom = Empleado.Apellidom,
                    SexoId = Empleado.SexoId,
                    Nss = Empleado.Nss,
                    Rfc = Empleado.Rfc,
                    Telefono = Empleado.Telefono,
                    Email = Empleado.Email
                };

                // Agrega el empleado al contexto y guarda los cambios
                _context.Empleados.Add(nuevoEmpleado);
                _context.SaveChanges();

                ViewData["ConnectionMessage"] = "Datos guardados correctamente.";

                // Limpiar el formulario después de guardar los datos
                Empleado = new Empleado(); // Crea un nuevo objeto Empleado vacío
            }
            catch (Exception ex)
            {
                // Maneja errores al guardar en la base de datos
                _logger.LogError(ex, "Error al guardar en la base de datos");
                ViewData["ConnectionMessage"] = "Error al guardar los datos.";
            }

            // Redirige a la página actual para mostrar el mensaje
            return Page();
        }
    }
}