using DomainModel.DtoModels;
using Microsoft.AspNetCore.Components;
using System.Text.Json;
using System.Text;

namespace ParkingManagementBlazorServer.Services
{
    public class LoginService
    {
        private readonly HttpClient _httpClient;
        private readonly string BaseApiUrl = "https://localhost:7289/api/Login";
        private readonly CustomAuthenticationStateProvider _customAuthenticationStateProvider;
        private readonly NavigationManager _navigationManager;
        public LoginService(HttpClient httpClient, CustomAuthenticationStateProvider customAuthenticationStateProvider, NavigationManager navigationManager)
        {
            _httpClient = httpClient;
            _customAuthenticationStateProvider = customAuthenticationStateProvider;
            _navigationManager = navigationManager;
        }

        public async Task Login(UserLogin userLogin)
        {
            if (userLogin.UserName == null || userLogin.Password == null)
                Console.WriteLine("nesto nevalja");
            // LoginPageBase.message = "Username or password cannot be empty!";
            else
            {
                var httpPostRequest = new HttpRequestMessage(HttpMethod.Post, BaseApiUrl);
                httpPostRequest.Content = new StringContent(JsonSerializer.Serialize(userLogin), Encoding.UTF8, "application/json");
                var httpResponseMessage = await _httpClient.SendAsync(httpPostRequest);

                var jwtToken = httpResponseMessage.Content.ReadAsStringAsync();
                if (jwtToken.Result != "User not found")
                {
                    await _customAuthenticationStateProvider.SetTokenAsync(jwtToken.Result);
                    //var identity = await _customAuthenticationStateProvider.GetAuthenticationStateAsync();
                    //var claims = identity.User.Identities.First().Claims.ToList();
                    //if (claims[2].Value == "Employee")
                    //    _navigationManager.NavigateTo("/Employee");
                    //else
                    //    _navigationManager.NavigateTo("/Admin");
                    _navigationManager.NavigateTo("/");
                }
                else
                {
                    // LoginPageBase.message = "Incorrect username or password!";
                    _navigationManager.NavigateTo("/");
                }
            }
        }
    }
}
