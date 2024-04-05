﻿using System;
using System.Collections.Generic;

namespace BackendApi.Models;

public partial class MetodosPago
{
    /// <summary>
    /// Identificador único del método de pago.
    /// </summary>
    public int IdMetodoPago { get; set; }

    /// <summary>
    /// Nombre del método de pago.
    /// </summary>
    public string? Nombre { get; set; }

    /// <summary>
    /// Descripción del método de pago.
    /// </summary>
    public string? Descripcion { get; set; }

    public virtual ICollection<Compra> Compras { get; set; } = new List<Compra>();
}