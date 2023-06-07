using ParkingLot.core.Interfaces;
using ParkingLot.core.Services;
using ParkingLot.core.Utilities;
using ParkingLot.Infrastructure;
using ParkingLot.Infrastructure.Repository;

namespace ParkingLot.Extentions
{
    public static class ServiceExtensions
    {
        public static void AddServiceLifeTime(this IServiceCollection services)
        {
            // Register services here
            services.AddScoped<IDbInitializer, DbInitializer>();
            services.AddScoped<ILocationRepository, LocationRepository>(); 
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IParkingServiceHelper, ParkingServiceHelper>();
            services.AddScoped<ITicketService, TicketService>();
            services.AddScoped<IParkingService, ParkingService>();
            services.AddScoped<ICalculateFee, CalculateFee>();  
        }
    }
}
