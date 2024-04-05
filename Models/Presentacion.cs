using System;
using System.Collections.Generic;

namespace BackendApi.Models;

public partial class Presentacion
{
    /// <summary>
    /// Identificador único para cada tipo de presentación.
    /// </summary>
    public int IdPresentacion { get; set; }

    /// <summary>
    /// El nombre o tipo de presentación del producto.
    /// </summary>
    public string? NombrePresentacion { get; set; }

    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
}
