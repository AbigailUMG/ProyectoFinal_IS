using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BackendApi.Models;

public partial class Marca
{
    /// <summary>
    /// Identificador único para cada marca.
    /// </summary>
    public int IdMarcas { get; set; }

    /// <summary>
    /// El nombre de la marca.
    /// </summary>
    public string NombreMarca { get; set; } = null!;

    /// <summary>
    /// El estado de la marca. si esta activo o desactivo
    /// </summary>
    public bool Estado { get; set; }

    /// <summary>
    /// Una descripción de la marca. Este campo puede tneer como maximo 150 caracteres 
    /// </summary>
    public string? Descripcion { get; set; }

    [JsonIgnore]
    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
}
