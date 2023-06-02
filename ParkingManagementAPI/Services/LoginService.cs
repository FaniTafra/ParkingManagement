using DomainModel.DtoModels;
using DomainModel.Models;
using Microsoft.IdentityModel.Tokens;
using ParkingManagementAPI.DatabaseContext;
using ParkingManagementAPI.Repository;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ParkingManagementAPI.Services
{
    public class LoginService
    {
        private IConfiguration _config;
        private readonly ParkingDbContext _parkingDbContext;
        private readonly UserRepository _userRepository;

        public LoginService(IConfiguration config, ParkingDbContext parkingDbContext, UserRepository userRepository)
        {
            _config = config;
            _parkingDbContext = parkingDbContext;
            _userRepository = userRepository;
        }
        public string Generate(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.Rsa, user.Id.ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.UserName)
            };
            var token = new JwtSecurityToken(_config["Jwt:Issuer"], _config["Jwt:Audience"], claims,
                expires: DateTime.UtcNow.AddMinutes(45),
                signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public User Authenticate(UserLogin userLogin)
        {
            var users = _userRepository.GetUsers();
            var currentUser = users.FirstOrDefault(u => u.UserName == userLogin.UserName && u.Password == HashUserPassword(userLogin.Password));
            //var currentUser = users.FirstOrDefault(u => u.UserName == userLogin.UserName && u.Password == userLogin.Password);
            return currentUser;
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

