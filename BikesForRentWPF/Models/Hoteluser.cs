using System;
using System.Collections.Generic;

namespace BikesForRentWPF.Models;

public partial class Hoteluser
{
    public int Id { get; set; }

    public int? HotelId { get; set; }

    public int? UserId { get; set; }

    public virtual Hotel? Hotel { get; set; }

    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();

    public virtual User? User { get; set; }
}
