using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace AMS.Web.Services
{
    public class AuthService
    {
        private readonly HttpClient _http;
        private readonly IJSRuntime _js;

        public AuthService(HttpClient http, IJSRuntime js)
        {
            _http = http;
            _js = js;
        }

        public async Task<bool> LoginAsync(string email, string password)
        {
            var response = await _http.PostAsJsonAsync("api/auth/login", new { Email = email, Password = password });

            if (!response.IsSuccessStatusCode)
                return false;

            // Deserialize response JSON
            var json = await response.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(json);
            var token = doc.RootElement.GetProperty("token").GetString();

            if (string.IsNullOrEmpty(token))
                return false;

            // ✅ Save token to localStorage
            await _js.InvokeVoidAsync("localStorage.setItem", "authToken", token);

            return true;
        }

        public async Task<string?> GetTokenAsync()
        {
            return await _js.InvokeAsync<string>("localStorage.getItem", "authToken");
        }

        public async Task LogoutAsync()
        {
            await _js.InvokeVoidAsync("localStorage.removeItem", "authToken");
        }
    }
}
