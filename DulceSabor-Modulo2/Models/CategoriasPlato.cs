using System;
using System.Collections.Generic;

namespace DulceSabor_Modulo2.Models;

public partial class CategoriasPlato
{
    public int CategoriaId { get; set; }

    public string? Nombre { get; set; }

    public virtual ICollection<Plato> Platos { get; set; } = new List<Plato>();
}
