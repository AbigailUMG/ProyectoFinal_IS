using System;
using System.Collections.Generic;

namespace BackendApi.Models;

public partial class ListadoPedido
{
    /// <summary>
    /// Identificador único para cada listado de pedidos.
    /// </summary>
    public long IdListado { get; set; }

    /// <summary>
    /// La fecha en que se creó el listado de pedidos.
    /// </summary>
    public DateTime FechaCreacion { get; set; }

    /// <summary>
    /// Llave foránea que se relaciona con el empleado que creó el listado.
    /// </summary>
    public int FKEmpleado { get; set; }

    /// <summary>
    /// El estado del listado de pedidos. puede ser completado, en proceso, en espera ente otras
    /// </summary>
    public bool Estado { get; set; }

    public virtual ICollection<DetalleListadoDePedido> DetalleListadoDePedidos { get; set; } = new List<DetalleListadoDePedido>();

    public virtual Usuario FKEmpleadoNavigation { get; set; } = null!;
}
