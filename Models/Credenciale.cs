using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BackendApi.Models;

public partial class Credenciale
{
    public string? Username { get; set; }

    public string? Password { get; set; }

    public int IdCredenciales { get; set; }

    public bool? Estado { get; set; }

    public int? FkRol { get; set; }

    [JsonIgnore]
    public virtual Rol? FkRolNavigation { get; set; }

    [JsonIgnore]
    public virtual Usuario IdCredencialesNavigation { get; set; } = null!;
}
