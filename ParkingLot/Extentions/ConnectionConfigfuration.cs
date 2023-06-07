using Microsoft.EntityFrameworkCore;
using ParkingLot.Infrastructure;

namespace ParkingLot.Extentions
{
    public static class ConnectionConfigfuration
    {
        public static void AddDbContextAndConfiguration(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContextPool<ApplicationDbContext>(options => options.UseSqlServer(config.GetConnectionString("DefaultConnection")));
        }
    }
}
