using System;
using System.Collections.Generic;

namespace DulceSabor_Modulo2.Models;

public partial class Estado
{
    public int IdEstado { get; set; }

    public string TipoEstado { get; set; } = null!;

    public virtual ICollection<Empleado> Empleados { get; set; } = new List<Empleado>();
}
