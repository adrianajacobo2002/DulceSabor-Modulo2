using System;
using System.Collections.Generic;

namespace DulceSabor_Modulo2.Models;

public partial class Combo
{
    public int ComboId { get; set; }

    public string? Nombre { get; set; }

    public string? Descripcion { get; set; }

    public decimal? Precio { get; set; }

    public int? Disponible { get; set; }

    public virtual ICollection<DetalleCuenta> DetalleCuenta { get; set; } = new List<DetalleCuenta>();

    public virtual ICollection<Plato> Platos { get; set; } = new List<Plato>();
}
