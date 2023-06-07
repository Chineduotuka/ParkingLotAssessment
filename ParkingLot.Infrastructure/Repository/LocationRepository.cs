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
    public class LocationRepository: GenericRepository<Location>,ILocationRepository
    {
        private readonly ApplicationDbContext _context;
        public LocationRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Location> GetLocationByName(string locationName)
        {
            return await _context.locations.SingleOrDefaultAsync(v => v.Name == locationName) ?? new Location();
        }
    }
}
