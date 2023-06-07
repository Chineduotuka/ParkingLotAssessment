using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingLot.Domain.Models
{
    public class Location
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Vehicle>? Vehicles { get; set; }
        public int TotalAvailableSpots { get; set; }
        public int MotorcyclesAndScootersAvailableSpots { get; set; }
        public int SuvsAndCarsAvailableSpots { get; set; }
        public int BusesAndTrucksAvailableSpots { get; set; }
        public int DailyCount { get; set; }
        public DateTime LastIssuedTicket { get; set; }
        public bool IsAvailable { get; set; }
    }
}
