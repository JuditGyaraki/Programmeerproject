using BikesForRentWPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikesForRentWPF
{
    public class ReservationBike
    {
        
        public int Id { get; set; }

        public int ReservationId { get; set; }

        public int BikeId { get; set; }

        public Reservation Reservation { get; set; }

        public Bike Bike { get; set; }
    }
}

