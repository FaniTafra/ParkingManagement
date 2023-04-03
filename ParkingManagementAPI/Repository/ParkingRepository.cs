using DomainModel.Models;
using ParkingManagementAPI.DatabaseContext;

namespace ParkingManagementAPI.Repository
{
    public class ParkingRepository
    {
        private readonly ParkingDbContext _parkingDbContext;
        public ParkingRepository(ParkingDbContext parkingDbContext)
        {
            _parkingDbContext = parkingDbContext;
        }
        public List<Parking> GetParkings()
        {
            return _parkingDbContext.Parking.ToList();
        }
        public void InsertParking (Parking parking)
        {
            _parkingDbContext.Parking.Add(parking);
            _parkingDbContext.SaveChanges();
        }
    }
}
