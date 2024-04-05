using System;
using System.Collections.Generic;

namespace BackendApi.Models;

public partial class Compra
{
    /// <summary>
    /// Identificador único de la compra.
    /// </summary>
    public long IdCompra { get; set; }

    /// <summary>
    /// Fecha de la compra.
    /// </summary>
    public DateTime? FechaCompra { get; set; }

    /// <summary>
    /// Llave foránea que hace referencia al proveedor de la compra.
    /// </summary>
    public int? FkProveedor { get; set; }

    /// <summary>
    ///  Llave foránea que hace referencia al empleado que realizó la compra.
    /// </summary>
    public int? FkUsuario { get; set; }

    /// <summary>
    ///  Llave foránea que hace referencia al método de pago utilizado.
    /// </summary>
    public int? FkMetodoPago { get; set; }

    /// <summary>
    /// Observaciones adicionales sobre la compra.
    /// </summary>
    public string? Observaciones { get; set; }

    public decimal? Total { get; set; }

    public virtual ICollection<DetalleCompra> DetalleCompras { get; set; } = new List<DetalleCompra>();

    public virtual MetodosPago? FkMetodoPagoNavigation { get; set; }

    public virtual Proveedore? FkProveedorNavigation { get; set; }

    public virtual Usuario? FkUsuarioNavigation { get; set; }
}
