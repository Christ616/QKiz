using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace QKiz.Pages
{
    public class Solicitud
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string ApellidoP { get; set; }
        public string ApellidoM { get; set; }
        public int Sexo_id { get; set; }
        public string NSS { get; set; }
        public string RFC { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
    }
    public class rhModel : PageModel
    {
        public List<Solicitud> Solicitudes { get; set; }

        private readonly ILogger<rhModel> _logger;
                
        public rhModel(ILogger<rhModel> logger)
        {
            Solicitudes = new List<Solicitud>();

            _logger = logger;
        }

        public void OnGet()
        {

        }
    }
}
