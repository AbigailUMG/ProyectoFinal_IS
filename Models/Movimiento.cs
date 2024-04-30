using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BackendApi.Models;

public partial class Movimiento
{
    /// <summary>
    /// Identificador único del tipo de movimiento.
    /// </summary>
    public int IdMovimiento { get; set; }

    /// <summary>
    /// Nombre del tipo de movimiento.
    /// </summary>
    public string Nombre { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<MovimientoCaja> MovimientoCajas { get; set; } = new List<MovimientoCaja>();
}
