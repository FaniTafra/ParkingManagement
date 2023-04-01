using DomainModel.Models;

namespace ParkingManagementBlazorServer.Services
{
    public class ParkingService
    {
        private readonly HttpClient _httpClient;
        private readonly string BaseApiUrl = "http://localhost:7289";
        public ParkingService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<Parking>> GetParkings()
        {
            return await _httpClient.GetFromJsonAsync<List<Parking>>(BaseApiUrl);
        }
    }
}
