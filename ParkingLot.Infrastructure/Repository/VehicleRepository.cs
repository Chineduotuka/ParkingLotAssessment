using Microsoft.EntityFrameworkCore;
using ParkingLot.core.Interfaces;
using ParkingLot.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingLot.Infrastructure.Repository
{
    public class VehicleRepository : GenericRepository<Vehicle>, IVehicleRepository
    {
        private readonly ApplicationDbContext _context;
        public VehicleRepository(ApplicationDbContext context) : base(context)
        {
            _context= context;
        }

        public async Task<Vehicle> GetVehicleByTicketNumber(string number)
        {
            return await _context.vehicles.SingleOrDefaultAsync(v => v.TicketNumber == number) ?? new Vehicle();
        }
    }
}
