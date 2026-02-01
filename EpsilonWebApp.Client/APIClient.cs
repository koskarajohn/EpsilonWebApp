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

    
    public async Task<BaseResponse> DeleteAsync(string url) 
    {
        var response = new BaseResponse();
        
        try
        {
            var apiResponseMessage = await _httpClient.DeleteAsync(url);
            
            if (apiResponseMessage.IsSuccessStatusCode)
                return response;
            
            var info = await apiResponseMessage.Content.ReadFromJsonAsync<APIErrorResponse>();
            response.Success = false;
            response.ErrorMessage = info.Title;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            response.Success = false;
            response.ErrorMessage = ex.Message;
        }

        return response;
    }
    
    public async Task<BaseResponse> PutAsync(string url, object data) 
    {
        var response = new BaseResponse();
        
        try
        {
            var apiResponseMessage = await _httpClient.PutAsJsonAsync(url, data);
            
            if (apiResponseMessage.IsSuccessStatusCode)
                return response;
            
            var info = await apiResponseMessage.Content.ReadFromJsonAsync<APIErrorResponse>();
            response.Success = false;
            response.ErrorMessage = info.Title;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            response.Success = false;
            response.ErrorMessage = ex.Message;
        }

        return response;
    }

    public record APIErrorResponse(string Title, int Status);
}