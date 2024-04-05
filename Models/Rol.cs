using System;
using System.Collections.Generic;

namespace BackendApi.Models;

public partial class Rol
{
    /// <summary>
    /// Identificador único para cada puesto en la tienda.
    /// </summary>
    public int IdRol { get; set; }

    /// <summary>
    /// El nombre del puesto.
    /// </summary>
    public string NombrePuesto { get; set; } = null!;

    /// <summary>
    /// Cualquier información adicional sobre el puesto.
    /// </summary>
    public string? Detalles { get; set; }

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
