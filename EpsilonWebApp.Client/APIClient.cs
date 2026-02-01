using System.Net.Http.Json;
using System.Runtime.InteropServices.JavaScript;

namespace EpsilonWebApp.Client;

public class APIClient
{
    private readonly HttpClient _httpClient;

    public APIClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    public async Task<T?> GetAsync<T>(string url) where T : new()
    {
        var responseObject = new T();
        
        try
        {
            var response = await _httpClient.GetAsync(url);
            
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadFromJsonAsync<T>();
                responseObject = data;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }

        return responseObject;
    }
}