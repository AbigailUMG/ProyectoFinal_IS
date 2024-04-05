using System;
using System.Collections.Generic;

namespace BackendApi.Models;

public partial class DetalleCompra
{
    /// <summary>
    /// Identificador único del detalle de compra.
    /// </summary>
    public long IdDetalleCompra { get; set; }

    /// <summary>
    /// Llave foránea que hace referencia al producto en la compra.
    /// </summary>
    public int? FkProducto { get; set; }

    /// <summary>
    /// Llave foránea que hace referencia a la compra a la que pertenece el detalle.
    /// </summary>
    public long? FkCompra { get; set; }

    /// <summary>
    /// Descuentos aplicados en el detalle de compra.
    /// </summary>
    public decimal? Descuentos { get; set; }

    /// <summary>
    ///  Cantidad comprada del producto.
    /// </summary>
    public int CantidadCompra { get; set; }

    /// <summary>
    /// Precio unitario de compra del producto.
    /// </summary>
    public decimal PrecioUnitarioCompra { get; set; }

    /// <summary>
    /// Precio sugerido de venta del producto.
    /// </summary>
    public decimal PrecioSugeridoVenta { get; set; }

    public int NoLote { get; set; }

    public virtual Compra? FkCompraNavigation { get; set; }

    public virtual Producto? FkProductoNavigation { get; set; }
}
