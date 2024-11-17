using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;

namespace GoT_API
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<T> FetchDataAsync<T>(string url)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Accept.Clear();
                _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/vnd.anapioficeandfire+json", 1));

                HttpResponseMessage response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                string data = await response.Content.ReadAsStringAsync();

                //--------- Logg
                //Console.WriteLine($"{data}\n-------------------------------------------");
                //---------

                T result = JsonSerializer.Deserialize<T>(data, new JsonSerializerOptions { PropertyNameCaseInsensitive = true});
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Problem when fetching data: {e.Message}");
                return default;
            }
        }
    }
}
