using System;
using System.Collections.Generic;

namespace BackendApi.Models;

public partial class MovimientoCaja
{
    /// <summary>
    /// Identificador único del movimiento de caja.
    /// </summary>
    public int IdMovCaja { get; set; }

    /// <summary>
    /// Fecha del movimiento de caja.
    /// </summary>
    public DateTime Fecha { get; set; }

    /// <summary>
    /// Descripción del movimiento de caja.
    /// </summary>
    public string Descripcion { get; set; } = null!;

    /// <summary>
    /// Descripción del movimiento de caja.
    /// </summary>
    public string? Observaciones { get; set; }

    /// <summary>
    /// Llave foránea que hace referencia al tipo de movimiento.
    /// </summary>
    public int FkTipoMovimiento { get; set; }

    /// <summary>
    /// Monto total del movimiento de caja.
    /// </summary>
    public decimal MontoTotal { get; set; }

    public virtual Movimiento FkTipoMovimientoNavigation { get; set; } = null!;
}
