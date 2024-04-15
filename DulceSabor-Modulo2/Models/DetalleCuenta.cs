using System;
using System.Collections.Generic;

namespace DulceSabor_Modulo2.Models;

public partial class DetalleCuenta
{
    public int IdDetalleCuenta { get; set; }

    public int IdCuenta { get; set; }

    public int? IdPlato { get; set; }

    public int? IdCombo { get; set; }

    public int? IdPromocion { get; set; }

    public decimal Precio { get; set; }

    public decimal? Descuento { get; set; }

    public string? Comentario { get; set; }

    public string? Estado { get; set; }

    public virtual Combo? IdComboNavigation { get; set; }

    public virtual Cuenta IdCuentaNavigation { get; set; } = null!;

    public virtual Plato? IdPlatoNavigation { get; set; }

    public virtual Promocione? IdPromocionNavigation { get; set; }
}
