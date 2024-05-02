using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BackendApi.Models;

public partial class Producto
{
    /// <summary>
    /// Identificador único para cada producto.
    /// </summary>
    public int IdProductos { get; set; }

    /// <summary>
    /// El nombre del producto.
    /// </summary>
    public string NombreProducto { get; set; } = null!;

    /// <summary>
    /// Una descripción del producto.
    /// </summary>
    public string? Descripcion { get; set; }

    /// <summary>
    /// El código de barras del producto.
    /// </summary>
    public string? CodigoBarras { get; set; }

    /// <summary>
    /// Tamaño del producto.
    /// </summary>
    public decimal Tamanio { get; set; }

    /// <summary>
    ///  Llave foránea que se relaciona con la presentación del producto.
    /// </summary>
    public int? FkPresentacion { get; set; }

    /// <summary>
    /// Llave foránea que se relaciona con la unidad de medida del producto.
    /// </summary>
    public int? FkUnidadMedida { get; set; }

    /// <summary>
    /// Llave foránea que se relaciona con la categoría del producto.
    /// </summary>
    public int? FkCategoria { get; set; }

    /// <summary>
    /// Llave foránea que se relaciona con la marca del producto.
    /// </summary>
    public int? FkMarca { get; set; }

    public bool? Estado { get; set; }

    [JsonIgnore]
    public virtual ICollection<DetalleCompra> DetalleCompras { get; set; } = new List<DetalleCompra>();

    [JsonIgnore]
    public virtual ICollection<DetalleVenta> DetalleVenta { get; set; } = new List<DetalleVenta>();

    [JsonIgnore]
    public virtual Categoria? FkCategoriaNavigation { get; set; }

    [JsonIgnore]
    public virtual Marca? FkMarcaNavigation { get; set; }

    [JsonIgnore]
    public virtual Presentacion? FkPresentacionNavigation { get; set; }

    [JsonIgnore]
    public virtual UnidadesMedida? FkUnidadMedidaNavigation { get; set; }

    [JsonIgnore]
    public virtual ICollection<Inventario> Inventarios { get; set; } = new List<Inventario>();
}
