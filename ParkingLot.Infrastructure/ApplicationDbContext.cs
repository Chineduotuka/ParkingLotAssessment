using Microsoft.EntityFrameworkCore;
using ParkingLot.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingLot.Infrastructure
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Vehicle> vehicles { get; set; }
        public DbSet<Location> locations { get; set; }
   
    }
}
