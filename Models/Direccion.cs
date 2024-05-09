using System;
using System.Collections.Generic;

namespace QKiz.Models;

public partial class Direccion
{
    public int Id { get; set; }

    public string Calle { get; set; } = null!;

    public string Numext { get; set; } = null!;

    public string? Numint { get; set; }

    public string Cp { get; set; } = null!;

    public string Colonia { get; set; } = null!;

    public string Ciudad { get; set; } = null!;

    public string Estado { get; set; } = null!;

    public virtual ICollection<Empleado> Empleados { get; set; } = new List<Empleado>();
}
