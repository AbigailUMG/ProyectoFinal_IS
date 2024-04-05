using System;
using System.Collections.Generic;

namespace BackendApi.Models;

public partial class Movimiento
{
    /// <summary>
    /// Identificador único del tipo de movimiento.
    /// </summary>
    public int IdMovimiento { get; set; }

    /// <summary>
    /// Nombre del tipo de movimiento.
    /// </summary>
    public string Nombre { get; set; } = null!;

    public virtual ICollection<MovimientoCaja> MovimientoCajas { get; set; } = new List<MovimientoCaja>();
}
