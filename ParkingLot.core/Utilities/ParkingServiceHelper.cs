using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParkingLot.core.Interfaces;

namespace ParkingLot.core.Utilities
{
    public class ParkingServiceHelper : IParkingServiceHelper
    {
        private const string v_suv = "suv";
        public async Task<bool> CanPark(string location, string carType)
        {
            if((carType.ToLowerInvariant() == "bus" || carType.ToLowerInvariant() == "truck") && 
                (location.ToLowerInvariant() == "stadium" || location.ToLowerInvariant() == "airport" ))
            {
                return false;
            }
            return true;
        }
        public async Task<string> GetVehicleClass(string carType)
        {
            if(carType.ToLowerInvariant() == "suv" || carType.ToLowerInvariant() == "car")
            {
                return "SuvsAndCarsAvailableSpots";
            }
            else if(carType.ToLowerInvariant() == "bus" || carType.ToLowerInvariant() == "truck")
            {
                return "BusesAndTrucksAvailableSpots";
            }
            else
            {
                return "MotorcyclesAndScootersAvailableSpots";
            }
        }
    }
}
