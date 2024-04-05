using System;
using System.Collections.Generic;

namespace BackendApi.Models;

public partial class Usuario
{
    /// <summary>
    /// Identificador único para cada empleado.
    /// </summary>
    public int IdUsuario { get; set; }

    /// <summary>
    /// Cualquier otro nombre del empleado.
    /// </summary>
    public string? Email { get; set; }

    /// <summary>
    /// El primer apellido del empleado.
    /// </summary>
    public string PrimerNombre { get; set; } = null!;

    /// <summary>
    /// El segundo apellido del empleado.
    /// </summary>
    public string? SegundoNombre { get; set; }

    /// <summary>
    /// Cualquier otro nombre del empleado.
    /// </summary>
    public string? OtrosNombres { get; set; }

    /// <summary>
    /// El primer apellido del empleado.
    /// </summary>
    public string? PrimerApellido { get; set; }

    /// <summary>
    /// El segundo apellido del empleado.
    /// </summary>
    public string? SegundoApellido { get; set; }

    public DateTimeOffset? FechaRegistro { get; set; }

    public virtual ICollection<Compra> Compras { get; set; } = new List<Compra>();

    public virtual Credenciale IdUsuarioNavigation { get; set; } = null!;

    public virtual ICollection<ListadoPedido> ListadoPedidos { get; set; } = new List<ListadoPedido>();

    public virtual ICollection<Venta> Venta { get; set; } = new List<Venta>();
}
