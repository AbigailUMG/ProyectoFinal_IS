using System;
using System.Collections.Generic;

namespace BackendApi.Models;

public partial class Venta
{
    /// <summary>
    /// Identificador único para cada venta.
    /// </summary>
    public long IdVenta { get; set; }

    /// <summary>
    /// La fecha en que se realizó la venta.
    /// 
    /// </summary>
    public DateTime? FechaVenta { get; set; }

    /// <summary>
    /// Llave foránea que se relaciona con el empleado que realizó la venta.
    /// </summary>
    public int? FkUsuario1 { get; set; }

    public decimal? Total { get; set; }

    public bool? Estado { get; set; }

    public virtual ICollection<DetalleVenta> DetalleVenta { get; set; } = new List<DetalleVenta>();

    public virtual Usuario? FkUsuario1Navigation { get; set; }
}
