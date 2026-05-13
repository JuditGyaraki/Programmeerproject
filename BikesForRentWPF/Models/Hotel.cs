using System;
using System.Collections.Generic;

namespace BikesForRentWPF.Models;

public partial class Hotel
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? City { get; set; }

    public string? Address { get; set; }

    public string? Telephone { get; set; }

    public virtual ICollection<Bike> Bikes { get; set; } = new List<Bike>();

    public virtual ICollection<Hoteluser> Hotelusers { get; set; } = new List<Hoteluser>();
}
