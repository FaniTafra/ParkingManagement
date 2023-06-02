using DomainModel.Models;
using ParkingManagementAPI.Controllers;
using ParkingManagementAPI.Repository;
using System.Security.Cryptography;
using System.Text;

namespace ParkingManagementAPI.Services
{
    public class UserService
    {
        private readonly UserController _userController;
        private readonly UserRepository _userRepository;
        public UserService(UserRepository userRepository) 
        {
            _userRepository = userRepository;
        }
        public void RegisterUser(User user)
        {
            user.Password = HashUserPassword(user.Password);
            _userRepository.InsertUser(user);
        }

        private string HashUserPassword(string plainTextPassword)
        {
            string hashedPassword;
            using (SHA256 sha256Hash = SHA256.Create())
            {
                hashedPassword = GetHash(sha256Hash, plainTextPassword);
            }
            return hashedPassword;
        }
        private static string GetHash(HashAlgorithm hashAlgorithm, string input)
        {

            byte[] data = hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(input));

            var sBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }
    }
}
