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
        //Static HttpClient instance to reuse connections and improve performance.
        private static readonly HttpClient _httpClient = new HttpClient();

        //Method that tries to fetch data
        public async Task<T> FetchDataAsync<T>(string url)
        {
            try
            {
                //Set request headers
                _httpClient.DefaultRequestHeaders.Accept.Clear();
                _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/vnd.anapioficeandfire+json", 1));

                //Send GET request and ensure success
                HttpResponseMessage response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                //--------- Logg data
                //Console.WriteLine($"{data}\n-------------------------------------------");
                //---------

                //Read data and deserialize response
                string data = await response.Content.ReadAsStringAsync();
                T result = JsonSerializer.Deserialize<T>(data, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }) ?? throw new Exception("Deserialized data is null!");
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine($"\nProblem when fetching data. Returning null values: {e.Message}\n");
                return default;//Exception returns null
            }
        }
    }
}