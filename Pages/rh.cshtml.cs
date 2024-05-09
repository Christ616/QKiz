using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using QKiz.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QKiz.Pages
{
    public class rhModel : PageModel
    {
        private readonly BdQkizContext _context;

        public List<Empleado> Empleados { get; set; }

        public rhModel(BdQkizContext context)
        {
            _context = context;
            Empleados = new List<Empleado>();
        }

        public async Task OnGetAsync()
        {
            // Obtener la lista de empleados desde la base de datos
            Empleados = await _context.Empleados.ToListAsync();
        }
    }
}
