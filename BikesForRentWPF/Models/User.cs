using System;
using System.Collections.Generic;

namespace BikesForRentWPF.Models;

public partial class User
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Username { get; set; }

    public byte[]? Password { get; set; }

    public string? Email { get; set; }

    public int? RoleId { get; set; }

    public virtual ICollection<Hoteluser> Hotelusers { get; set; } = new List<Hoteluser>();

    public virtual Role? Role { get; set; }
}
