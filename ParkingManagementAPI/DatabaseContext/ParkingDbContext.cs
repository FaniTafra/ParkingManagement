using DomainModel.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace ParkingManagementAPI.DatabaseContext
{
    public class ParkingDbContext : DbContext
    {
        public ParkingDbContext(DbContextOptions<ParkingDbContext> dbContextOptions) : base(dbContextOptions){ }
        public DbSet<User> Users { get; set; }
        public DbSet<Parking> Parking { get; set; }
    }
}
