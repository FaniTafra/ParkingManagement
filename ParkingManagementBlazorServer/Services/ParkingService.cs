using DomainModel.Models;
using System.Text.Json;
using System.Text;
using System.Net.Http.Headers;

namespace ParkingManagementBlazorServer.Services
{
    public class ParkingService
    {
        private readonly HttpClient _httpClient;
        private readonly CustomAuthenticationStateProvider _customAuthenticationStateProvider;
        private readonly string BaseApiUrl = "https://localhost:7289/api/Parking";
        public ParkingService(HttpClient httpClient, CustomAuthenticationStateProvider customAuthenticationStateProvider)
        {
            _httpClient = httpClient;
            _customAuthenticationStateProvider = customAuthenticationStateProvider;
        }
        public async Task<List<Parking>> GetParkings()
        {
            return await _httpClient.GetFromJsonAsync<List<Parking>>(BaseApiUrl);
        }
        public async Task<List<Parking>> GetActiveParkings()
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _customAuthenticationStateProvider.GetTokenAsync());
            int userId = await _customAuthenticationStateProvider.GetUserId();
            return await _httpClient.GetFromJsonAsync<List<Parking>>($"{BaseApiUrl}/active/{userId}");
        }
        public async Task<List<Parking>> GetArchivedParkings()
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _customAuthenticationStateProvider.GetTokenAsync());
            int userId = await _customAuthenticationStateProvider.GetUserId();
            return await _httpClient.GetFromJsonAsync<List<Parking>>($"{BaseApiUrl}/archived/{userId}");
        }
        public async Task AddParkingAsync(Parking parking)
        {
            parking.OwnerId = await _customAuthenticationStateProvider.GetUserId();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _customAuthenticationStateProvider.GetTokenAsync());
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

        public async Task SelectingParkingAsync(int SelectedId)
        {
            int userId = await _customAuthenticationStateProvider.GetUserId();
            var httpRequest = new HttpRequestMessage(HttpMethod.Put, $"{BaseApiUrl}/{SelectedId}/{userId}");
            await _httpClient.SendAsync(httpRequest);
        }

        public async Task<List<Parking>> GetSelectedParkings()
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _customAuthenticationStateProvider.GetTokenAsync());
            int userId = await _customAuthenticationStateProvider.GetUserId();
            return await _httpClient.GetFromJsonAsync<List<Parking>>($"{BaseApiUrl}/selected/{userId}");
        }

        public async Task CancelParking(int parkingId)
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Put, $"{BaseApiUrl}/cancel/{parkingId}");
            await _httpClient.SendAsync(httpRequest);
        }
    }
}
