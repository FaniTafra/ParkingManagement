using DomainModel.Models;
using ParkingManagementAPI.DatabaseContext;
using DomainModel.Enums;

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
        public void InsertParking(Parking parking)
        {
            _parkingDbContext.Parking.Add(parking);
            _parkingDbContext.SaveChanges();
        }
        public Parking GetParking(int parkingId)
        {
            return _parkingDbContext.Parking.
                FirstOrDefault(parking => parking.Id.Equals(parkingId));
        }
        public void UpdateParking(Parking parking)
        {
            var parkingForUpdate = GetParking(parking.Id);
            if (parkingForUpdate != null)
            {
                parkingForUpdate.City = parking.City;
                parkingForUpdate.Neighborhood = parking.Neighborhood;
                parkingForUpdate.ParkingFrom = parking.ParkingFrom;
                parkingForUpdate.ParkingTo = parking.ParkingTo;
                _parkingDbContext.SaveChanges();
            }
        }
        public void DeleteParking(int parkingId)
        {
            var parkingForDelete = GetParking(parkingId);
            if (parkingForDelete != null)
            {
                _parkingDbContext.Parking.Remove(parkingForDelete);
                _parkingDbContext.SaveChanges();
            }
        }
        public List<Parking> GetActiveParkings()
        {
            var activeParkings = _parkingDbContext.Parking.Where(p => p.Status == ParkingStatus.Free || p.Status == ParkingStatus.Approved || p.Status == ParkingStatus.InUse).ToList();
            return activeParkings;
        }
        public List<Parking> GetArchivedParkings()
        {
            var archivedParkings = _parkingDbContext.Parking.Where(p => p.Status == ParkingStatus.Archived).ToList();
            return archivedParkings;
        }
    }
}
