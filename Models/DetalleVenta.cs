using System;
using System.Collections.Generic;

namespace BackendApi.Models;

public partial class DetalleVenta
{
    /// <summary>
    /// Identificador único para cada detalle de venta.
    /// </summary>
    public int IdDetalleVenta { get; set; }

    /// <summary>
    /// Llave foránea que se relaciona con la venta a la que pertenece el detalle.
    /// </summary>
    public long? FkVenta { get; set; }

    /// <summary>
    ///  Llave foránea que se relaciona con el producto vendido.
    /// </summary>
    public int? FkIdProducto1 { get; set; }

    public int CantidadProducto { get; set; }

    public decimal PrecioUnitario { get; set; }

    public decimal? Descuento { get; set; }

    public virtual Producto? FkIdProducto1Navigation { get; set; }

    public virtual Venta? FkVentaNavigation { get; set; }
}
