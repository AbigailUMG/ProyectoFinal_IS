using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BackendApi.Models;

public partial class Proveedore
{
    /// <summary>
    /// Identificador único para cada proveedor.
    /// </summary>
    public int IdProveedor { get; set; }

    /// <summary>
    /// El nombre del proveedor.
    /// </summary>
    public string? NombreProveedor { get; set; }

    /// <summary>
    /// El número de teléfono del proveedor.
    /// </summary>
    public string? Telefono { get; set; }

    /// <summary>
    /// El estado del proveedor. puede ser activo o inactivo  entre otras
    /// </summary>
    public bool? Estado { get; set; }

    /// <summary>
    /// El día en que se realiza la visita al proveedor.
    /// </summary>
    public string? DiaVisita { get; set; }

    /// <summary>
    /// El día de entrega preferido por el proveedor.
    /// </summary>
    public string? DiaEntrega { get; set; }

    /// <summary>
    /// Cualquier descripción adicional del proveedor. Maximo 100 caracteres.
    /// </summary>
    public string? Descripcion { get; set; }

    [JsonIgnore]
    public virtual ICollection<Compra> Compras { get; set; } = new List<Compra>();
}
