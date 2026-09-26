using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Animal_Spa.Services
{
    public class ApiClient
    {
        private readonly HttpClient _httpClient;

        public const string BaseUrl = "http://localhost:5052/";

        public ApiClient()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(BaseUrl),
                Timeout = TimeSpan.FromSeconds(10)
            };
        }

        public async Task<(bool Success, string Message)> CheckHealthAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("health");

                if (response.IsSuccessStatusCode)
                {
                    return (
                        true,
                        $"Connected ({(int)response.StatusCode})"
                    );
                }

                return (
                    false,
                    $"API returned {(int)response.StatusCode} " +
                    $"{response.ReasonPhrase}"
                );
            }
            catch (TaskCanceledException)
            {
                return (false, "Request timeout.");
            }
            catch (HttpRequestException ex)
            {
                return (false, $"Cannot connect to API: {ex.Message}");
            }
            catch (Exception ex)
            {
                return (false, $"Unexpected error: {ex.Message}");
            }
        }
    }
}