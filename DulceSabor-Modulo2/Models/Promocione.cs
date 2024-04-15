using System;
using System.Collections.Generic;

namespace DulceSabor_Modulo2.Models;

public partial class Promocione
{
    public int PromocionId { get; set; }

    public string? Nombre { get; set; }

    public string? Descripcion { get; set; }

    public decimal? Descuento { get; set; }

    public DateOnly? FechaInicio { get; set; }

    public DateOnly? FechaFin { get; set; }

    public virtual ICollection<DetalleCuenta> DetalleCuenta { get; set; } = new List<DetalleCuenta>();

    public virtual ICollection<Plato> Platos { get; set; } = new List<Plato>();
}
