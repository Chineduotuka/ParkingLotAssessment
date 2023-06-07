using ParkingLot.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingLot.core.Interfaces
{
    public interface ILocationRepository:IGenericRepository<Location>
    {
        Task<Location> GetLocationByName(string locationName);
    }
}
