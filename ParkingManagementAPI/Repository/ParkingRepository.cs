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
            return _parkingDbContext.Parking.OrderBy(p => p.ParkingFrom).ToList();
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
        public List<Parking> GetActiveParkings(int userId)
        {
            var activeParkings = _parkingDbContext.Parking.Where(p => (p.Status == ParkingStatus.Free || p.Status == ParkingStatus.Approved || p.Status == ParkingStatus.InUse) && p.OwnerId == userId).OrderBy(p => p.ParkingFrom).ToList();
            return activeParkings;
        }
        public List<Parking> GetArchivedParkings(int userId)
        {
            var archivedParkings = _parkingDbContext.Parking.Where(p => p.Status == ParkingStatus.Archived && p.OwnerId == userId).OrderBy(p => p.ParkingFrom).ToList();
            return archivedParkings;
        }

        public void SelectingParking(int SelectedId, int userId)
        {
            var selectedParking = _parkingDbContext.Parking.FirstOrDefault(p => p.Id == SelectedId);
            if (selectedParking != null && selectedParking.OwnerId != userId)
            {
                selectedParking.UserId = userId;
                selectedParking.Status = ParkingStatus.Approved;
                _parkingDbContext.SaveChanges();
            }
        }

        public List<Parking> GetSelectedParkings(int userId)
        {
            var selectedParkings = _parkingDbContext.Parking.Where(p => (p.Status == ParkingStatus.Approved || p.Status == ParkingStatus.InUse || p.Status == ParkingStatus.Archived) && p.UserId == userId).ToList();
            return selectedParkings;
        }

        public void CancelParking(int parkingId)
        {
            var parkingForCancel = _parkingDbContext.Parking.FirstOrDefault(p => p.Id == parkingId);
            if (parkingForCancel != null)
            {
                parkingForCancel.UserId = null;
                _parkingDbContext.SaveChanges();
            }
        }
    }
}
