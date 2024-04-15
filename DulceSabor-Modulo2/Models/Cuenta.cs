using System;
using System.Collections.Generic;

namespace DulceSabor_Modulo2.Models;

public partial class Cuenta
{
    public int IdCuenta { get; set; }

    public int IdMesa { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public DateTime? FechaApertura { get; set; }

    public string? Estado { get; set; }

    public virtual ICollection<DetalleCuenta> DetalleCuenta { get; set; } = new List<DetalleCuenta>();
}
