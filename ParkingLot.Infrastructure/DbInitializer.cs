using ParkingLot.core.Interfaces;
using ParkingLot.Domain.Models;

namespace ParkingLot.Infrastructure
{
    public class DbInitializer : IDbInitializer
    {
        private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;

        public DbInitializer(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Initialize()
        {
            //await _context.Database.EnsureCreatedAsync();
            try
            {
                if (!_context.locations.Any())
                {
                    List<Location> locations = new List<Location>
                    {
                        new Location
                        {
                            Name = "Mall",
                            MotorcyclesAndScootersAvailableSpots = 100,
                            SuvsAndCarsAvailableSpots = 80,
                            BusesAndTrucksAvailableSpots = 40,
                            TotalAvailableSpots = 220,
                            IsAvailable = true
                        },
                        new Location
                        {
                            Name = "Stadium",
                            MotorcyclesAndScootersAvailableSpots = 100,
                            SuvsAndCarsAvailableSpots = 80,
                            BusesAndTrucksAvailableSpots = 0,
                            TotalAvailableSpots = 220,
                            IsAvailable = true
                        },
                        new Location
                        {
                            Name = "Airport",
                            MotorcyclesAndScootersAvailableSpots = 100,
                            SuvsAndCarsAvailableSpots = 80,
                            BusesAndTrucksAvailableSpots = 40,
                            TotalAvailableSpots = 220,
                            IsAvailable = true
                        }
                    };

                    await _context.locations.AddRangeAsync(locations);
                    await _context.SaveChangesAsync();
                 
                };
            }
            catch(Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
    }
}
