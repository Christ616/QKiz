using System;
using System.Collections.Generic;

namespace QKiz.Models;

public partial class Sexo
{
    public int Id { get; set; }

    public string Genero { get; set; } = null!;

    public virtual ICollection<Empleado> Empleados { get; set; } = new List<Empleado>();
}
