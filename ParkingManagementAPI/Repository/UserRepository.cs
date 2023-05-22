using DomainModel.Models;
using ParkingManagementAPI.DatabaseContext;

namespace ParkingManagementAPI.Repository
{
    public class UserRepository
    {
        private readonly ParkingDbContext _parkingDbContext;
        public UserRepository(ParkingDbContext parkingDbContext)
        {
            _parkingDbContext = parkingDbContext;
        }
        public List<User> GetUsers()
        {
            return _parkingDbContext.Users.ToList();
        }
        public void InsertUser(User user)
        {
            _parkingDbContext.Users.Add(user);
            _parkingDbContext.SaveChanges();
        }
    }
}
