using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace GoT_API
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<string> FetchDataAsync(string url)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(url);

                response.EnsureSuccessStatusCode();

                string data = await response.Content.ReadAsStringAsync();
                return data;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Problem med att hämta data: {e.Message}");
                return null;
            }
        }
    }
}
