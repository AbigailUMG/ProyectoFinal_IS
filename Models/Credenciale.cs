using System;
using System.Collections.Generic;

namespace BackendApi.Models;

public partial class Credenciale
{
    public string? Username { get; set; }

    public string? Password { get; set; }

    public int IdCredenciales { get; set; }

    public virtual Usuario? Usuario { get; set; }
}
