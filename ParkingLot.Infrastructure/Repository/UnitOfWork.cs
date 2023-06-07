using Microsoft.EntityFrameworkCore;
using ParkingLot.core.Interfaces;
using ParkingLot.core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingLot.Infrastructure.Repository
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private LocationRepository _locationRepository { get; set;} = null!;
        private VehicleRepository _vehicleRepository { get; set; } = null!;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }
        public ILocationRepository LocationRepository => _locationRepository??= new LocationRepository(_context);
        public IVehicleRepository VehicleRepository => _vehicleRepository??= new VehicleRepository(_context);

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
