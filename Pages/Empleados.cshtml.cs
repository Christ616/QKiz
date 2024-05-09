using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using QKiz.Modelo;
using static QKiz.Modelo.Db_QKizContext;

namespace QKiz.Pages
{
    public class EmpleadosModel : PageModel
    {
        private readonly Db_QKizContext _context;
        private readonly ILogger<EmpleadosModel> _logger;

        public EmpleadosModel(Db_QKizContext context, ILogger<EmpleadosModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        [BindProperty]
        public Empleado Empleado { get; set; }

        public void OnGet()
        {
        }

        [HttpPost]
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Empleados.Add(Empleado);
            _context.SaveChanges();
            System.Diagnostics.Debug.WriteLine("xdxdxdxdxd");

            ViewData["ConnectionMessage"] = "Datos guardados correctamente.";
            return Page();
        }
    }
}
