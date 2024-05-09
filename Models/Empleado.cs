using System;
using System.Collections.Generic;

namespace QKiz.Models;

public partial class Empleado
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellidop { get; set; } = null!;

    public string Apellidom { get; set; } = null!;

    public int? SexoId { get; set; }

    public string Nss { get; set; } = null!;

    public string Rfc { get; set; } = null!;

    public string Telefono { get; set; } = null!;

    public string Email { get; set; } = null!;

    public int? DireccionId { get; set; }

    public virtual Direccion? Direccion { get; set; }

    public virtual Sexo? Sexo { get; set; }
}
