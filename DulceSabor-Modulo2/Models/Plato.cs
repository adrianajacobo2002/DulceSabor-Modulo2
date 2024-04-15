using System;
using System.Collections.Generic;

namespace DulceSabor_Modulo2.Models;

public partial class Plato
{
    public int PlatoId { get; set; }

    public string? Nombre { get; set; }

    public string? Descripcion { get; set; }

    public decimal? Precio { get; set; }

    public string? Categoria { get; set; }

    public int? Disponible { get; set; }

    public int? CategoriaId { get; set; }

    public virtual CategoriasPlato? CategoriaNavigation { get; set; }

    public virtual ICollection<DetalleCuenta> DetalleCuenta { get; set; } = new List<DetalleCuenta>();

    public virtual ICollection<Combo> Combos { get; set; } = new List<Combo>();

    public virtual ICollection<Promocione> Promocions { get; set; } = new List<Promocione>();
}
