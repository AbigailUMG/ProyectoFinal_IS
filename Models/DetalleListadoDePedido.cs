using System;
using System.Collections.Generic;

namespace BackendApi.Models;

/// <summary>
/// Identificador único para cada detalle de pedido.
/// </summary>
public partial class DetalleListadoDePedido
{
    /// <summary>
    /// Identificador único para cada tipo de presentación.
    /// </summary>
    public long IdLsDetalleProducto { get; set; }

    /// <summary>
    /// Llave foránea que se relaciona con el listado de pedidos al que pertenece el detalle.
    /// </summary>
    public long FkListadoPedido { get; set; }

    /// <summary>
    /// Llave foránea que se relaciona con el producto pedido.
    /// </summary>
    public int FkProducto3 { get; set; }

    /// <summary>
    ///  La cantidad del producto que se va a comprar.
    /// </summary>
    public int CantidadAComprar { get; set; }

    public virtual ListadoPedido FkListadoPedidoNavigation { get; set; } = null!;

    public virtual Producto FkProducto3Navigation { get; set; } = null!;
}
