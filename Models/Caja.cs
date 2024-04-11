using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BackendApi.Models;

public partial class Caja
{
    /// <summary>
    ///  Identificador único de la caja.
    /// </summary>
    public int IdCaja { get; set; }

    /// <summary>
    /// Año al que corresponde la caja.
    /// </summary>
    public string Anio { get; set; } = null!;

    /// <summary>
    /// Mes al que corresponde la caja.
    /// </summary>
    public string Mes { get; set; } = null!;

    /// <summary>
    /// Total de gastos registrados en la caja.
    /// </summary>
    public decimal TotalGastos { get; set; }

    /// <summary>
    /// Total de ingresos registrados en la caja.
    /// </summary>
    public decimal TotalIngresos { get; set; }

    /// <summary>
    /// Total de la caja.
    /// </summary>

    [JsonIgnore]
    public decimal TotalCaja { get; set; }
}
