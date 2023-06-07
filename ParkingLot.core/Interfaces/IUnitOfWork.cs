using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingLot.core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ILocationRepository LocationRepository { get;}
        IVehicleRepository VehicleRepository { get;}
        Task SaveAsync();
    }
}
