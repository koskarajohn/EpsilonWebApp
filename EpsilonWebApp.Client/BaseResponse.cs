namespace EpsilonWebApp.Client;

public class BaseResponse
{
    public bool Success { get; set; } = true;
    public string? ErrorMessage { get; set; }
}