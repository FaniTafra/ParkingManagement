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
        public IEnumerable<Parking> GetParkings()
        {
            return _parkingDbContext.Parkings.ToList();
        }
    }
}
