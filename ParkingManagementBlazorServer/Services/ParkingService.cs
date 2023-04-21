using DomainModel.Models;
using System.Text.Json;
using System.Text;

namespace ParkingManagementBlazorServer.Services
{
    public class ParkingService
    {
        private readonly HttpClient _httpClient;
        private readonly string BaseApiUrl = "https://localhost:7289/api/Parking";
        public ParkingService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<Parking>> GetParkings()
        {
            return await _httpClient.GetFromJsonAsync<List<Parking>>(BaseApiUrl);
        }
        public async Task<List<Parking>> GetActiveParkings()
        {
            return await _httpClient.GetFromJsonAsync<List<Parking>>($"{BaseApiUrl}/active");
        }
        public async Task<List<Parking>> GetArchivedParkings()
        {
            return await _httpClient.GetFromJsonAsync<List<Parking>>($"{BaseApiUrl}/archived");
        }
        public async Task AddParkingAsync(Parking parking)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, BaseApiUrl);
            request.Content = new StringContent(JsonSerializer.Serialize(parking), Encoding.UTF8,
            "application/json");
            await _httpClient.SendAsync(request);
        }
        public async Task<Parking> GetParkingAsync(int parkingId)
        {
            return await _httpClient.GetFromJsonAsync<Parking>($"{BaseApiUrl}/{parkingId}");
        }
        public async Task UpdateParkingAsync(Parking parking)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, BaseApiUrl);
            request.Content = new StringContent(JsonSerializer.Serialize(parking), Encoding.UTF8,
            "application/json");
            await _httpClient.SendAsync(request);
        }
        public async Task DeleteParking(int parkingId)
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Delete, $"{BaseApiUrl}/{parkingId}");
            await _httpClient.SendAsync(httpRequest);
        }

    }
}
