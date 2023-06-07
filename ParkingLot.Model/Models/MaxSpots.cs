using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingLot.Domain.Models
{
    public class MaxSpots
    {
        public const int MotorcyclesAndScooters = 100;
        public const int SuvsAndCars = 80;
        public const int BusesAndTrucks = 40;
        public const int TotalSpots = MotorcyclesAndScooters + SuvsAndCars + BusesAndTrucks;
    }
}
