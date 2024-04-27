using System;
using System.Collections.Generic;

namespace DulceSabor_Modulo2.Models;

public partial class Mesa
{
    public int Id { get; set; }

    public int? NumeroAsientos { get; set; }

    public int? NumeroMesa { get; set; }

    public bool? Estado { get; set; }
}
