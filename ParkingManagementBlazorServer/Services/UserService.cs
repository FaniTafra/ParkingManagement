using System.Net.Http;
using System.Text.Json;
using System.Text;
using DomainModel.Models;


    public class UserService
    {
        private readonly HttpClient _httpClient;
        private readonly string BaseApiUrl = "https://localhost:7289/api/User";
        public UserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task AddUserAsync(User user)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, BaseApiUrl);
            request.Content = new StringContent(JsonSerializer.Serialize(user), Encoding.UTF8, "application/json");
            await _httpClient.SendAsync(request);
        }
    }

