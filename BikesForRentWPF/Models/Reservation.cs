using System;
using System.Collections.Generic;

namespace BikesForRentWPF.Models;

public partial class Reservation
{
    public int Id { get; set; }

    public int? HoteluserId { get; set; }

    public int? BikeId { get; set; }

    public double? Totalprice { get; set; }

    public string? Startdate { get; set; }

    public string? Enddate { get; set; }

    public string? Status { get; set; }

    public int? Days { get; set; }

    public virtual Bike? Bike { get; set; }

    public virtual Hoteluser? Hoteluser { get; set; }
}
