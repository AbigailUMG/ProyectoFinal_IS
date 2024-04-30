using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BackendApi.Models;

public partial class UnidadesMedida
{
    /// <summary>
    /// Identificador único para cada unidad de medida.
    /// </summary>
    public int IdMedicion { get; set; }

    /// <summary>
    /// Descripción de la unidad de medida.
    /// </summary>
    public string Descripcion { get; set; } = null!;

    /// <summary>
    /// Prefijo asociado a la unidad de medida.
    /// </summary>
    public string Prefijo { get; set; } = null!;

    public bool? Estado { get; set; }

    [JsonIgnore]
    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
}
