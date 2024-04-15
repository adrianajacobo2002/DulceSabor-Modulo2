using System;
using System.Collections.Generic;

namespace DulceSabor_Modulo2.Models;

public partial class Empleado
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public string? Dui { get; set; }

    public DateOnly? FechaNacimiento { get; set; }

    public string? Telefono { get; set; }

    public string? Direccion { get; set; }

    public string? TelefonoReferencia { get; set; }

    public int? CargoId { get; set; }

    public int? EstadoId { get; set; }

    public decimal? Salario { get; set; }

    public DateOnly? FechaContratacion { get; set; }

    public virtual Cargo? Cargo { get; set; }

    public virtual Estado? Estado { get; set; }
}
