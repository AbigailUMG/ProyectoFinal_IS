using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BackendApi.Models;

public partial class Inventario
{
    /// <summary>
    /// Identificador único para cada registro en el inventario.
    /// </summary>
    public int IdInventario { get; set; }

    /// <summary>
    /// Llave foránea que se relaciona con el producto en el inventario.
    /// </summary>
    public int? FkProducto2 { get; set; }

    /// <summary>
    /// La fecha de la última salida de inventario
    /// </summary>
    public DateTime? FechaUltimaSalida { get; set; }

    /// <summary>
    ///  La fecha de la última entrada de inventario.
    /// </summary>
    public DateTime? FechaUltimaEntrada { get; set; }

    /// <summary>
    /// La cantidad total de stock disponible del producto en el inventario.
    /// </summary>
    public int? TotalStock { get; set; }

    public decimal? PrecioVenta { get; set; }

    [JsonIgnore]
    public virtual Producto? FkProducto2Navigation { get; set; }
}
