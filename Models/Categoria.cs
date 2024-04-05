using System;
using System.Collections.Generic;

namespace BackendApi.Models;

public partial class Categoria
{
    /// <summary>
    /// Identificador único para cada categoría de producto.
    /// </summary>
    public int IdCategoria { get; set; }

    /// <summary>
    /// El nombre de la categoría de producto.
    /// </summary>
    public string NombreCategoria { get; set; } = null!;

    /// <summary>
    /// Una descripción de la categoría.
    /// </summary>
    public string? Descripcion { get; set; }

    /// <summary>
    /// El estado de la categoría (por ejemplo, activa, inactiva, etc.).
    /// </summary>
    public bool Estado { get; set; }

    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
}
