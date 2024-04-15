using System;
using System.Collections.Generic;

namespace DulceSabor_Modulo2.Models;

public partial class Cargo
{
    public int IdCargo { get; set; }

    public string TipoCargo { get; set; } = null!;

    public virtual ICollection<Empleado> Empleados { get; set; } = new List<Empleado>();
}
