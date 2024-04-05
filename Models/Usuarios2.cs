using System;
using System.Collections.Generic;

namespace BackendApi.Models;

public partial class Usuarios2
{
    public int Id { get; set; }

    public string Password { get; set; } = null!;

    public DateTimeOffset? LastLogin { get; set; }

    public string Username { get; set; } = null!;

    public string Email { get; set; } = null!;

    public bool IsStaff { get; set; }

    public bool IsActive { get; set; }

    public DateTimeOffset DateJoined { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }
}
