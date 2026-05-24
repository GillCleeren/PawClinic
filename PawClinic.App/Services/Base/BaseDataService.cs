using Blazored.LocalStorage;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace PawClinic.App.Services.Base;

public abstract class BaseDataService
{
    protected readonly ILocalStorageService _localStorage;
    protected readonly HttpClient _httpClient;

    protected BaseDataService(HttpClient httpClient, ILocalStorageService localStorage)
    {
        _httpClient = httpClient;
        _localStorage = localStorage;
    }

    protected ApiResponse<T> ConvertApiExceptions<T>(HttpRequestException ex)
    {
        return new ApiResponse<T> { Message = "Something went wrong, please try again.", Success = false };
    }

    protected ApiResponse<T> BuildErrorResponse<T>(string message)
    {
        return new ApiResponse<T> { Message = message, Success = false };
    }

    protected async Task AddBearerToken()
    {
        if (await _localStorage.ContainKeyAsync("token"))
        {
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", await _localStorage.GetItemAsync<string>("token"));
        }
    }
}
