using System.ComponentModel.DataAnnotations;

namespace Crud.Models
{
    public class Empleado
    {
        // Esta clase es el modelo para mostrar la unión de las tablas empleados y direccion.
        // De igual forma se utiliza para actualizar y crear datos en esas tablas.

        public uint id_empleado { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Nombre")]
        [MinLength(2, ErrorMessage = "El campo {0} no puede ser menor a 2 caracteres")]
        public string? nombre { get; set; }
                 
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Apellido Paterno")]
        [MinLength(2, ErrorMessage = "El campo {0} no puede ser menor a 2 caracteres")]
        public string? apellidop { get; set; }

        [Display(Name = "Apellido Materno")]
        public string? apellidom { get; set; }

        [Display(Name = "Sexo")]
        public string? sexo_id { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "N° Seguro Social")]
        [MinLength(2, ErrorMessage = "El campo {0} no puede ser menor a 2 caracteres")]
        public string? nss { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "RFC")]
        [MinLength(2, ErrorMessage = "El campo {0} no puede ser menor a 2 caracteres")]
        public string? rfc { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Teléfono")]
        [MinLength(10, ErrorMessage = "El campo {0} no puede ser menor a 10 caracteres")]
        public string? telefono { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Email")]
        public string? email { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Calle")]
        public string? calle { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "N° Exterior")]
        public string? numext { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "N° Interior")]
        public string? numint { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Código postal")]
        public string? cp { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Colonia")]
        public string? colonia { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Ciudad")]
        public string? ciudad { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Estado")]
        public string? estado { get; set; }
    }
}
