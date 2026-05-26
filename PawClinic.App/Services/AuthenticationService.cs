using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using PawClinic.App.Auth;
using PawClinic.App.Contracts;
using PawClinic.App.Services.Base;
using PawClinic.App.ViewModels;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace PawClinic.App.Services;

public class AuthenticationService : BaseDataService, IAuthenticationService
{
    private readonly AuthenticationStateProvider _authenticationStateProvider;

    public AuthenticationService(
        HttpClient httpClient,
        ILocalStorageService localStorage,
        AuthenticationStateProvider authenticationStateProvider)
        : base(httpClient, localStorage)
    {
        _authenticationStateProvider = authenticationStateProvider;
    }

    public async Task<bool> Login(LoginViewModel loginViewModel)
    {
        try
        {
            var request = new AuthenticationRequest(loginViewModel.Email, loginViewModel.Password);
            var response = await _httpClient.PostAsJsonAsync("/api/account/authenticate", request);

            if (!response.IsSuccessStatusCode)
                return false;

            var authResponse = await response.Content.ReadFromJsonAsync<AuthenticationResponse>();

            if (authResponse is null || string.IsNullOrEmpty(authResponse.Token))
                return false;

            await _localStorage.SetItemAsync("token", authResponse.Token);
            ((CustomAuthenticationStateProvider)_authenticationStateProvider).SetUserAuthenticated(loginViewModel.Email);

            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> Register(RegisterViewModel registerViewModel)
    {
        try
        {
            var request = new RegistrationRequest(
                registerViewModel.FirstName,
                registerViewModel.LastName,
                registerViewModel.Email,
                registerViewModel.UserName,
                registerViewModel.Password);

            var response = await _httpClient.PostAsJsonAsync("/api/account/register", request);

            if (!response.IsSuccessStatusCode)
                return false;

            var registrationResponse = await response.Content.ReadFromJsonAsync<RegistrationResponse>();
            return !string.IsNullOrEmpty(registrationResponse?.UserId);
        }
        catch
        {
            return false;
        }
    }

    public async Task Logout()
    {
        await _localStorage.RemoveItemAsync("token");
        ((CustomAuthenticationStateProvider)_authenticationStateProvider).SetUserLoggedOut();
    }

    private record AuthenticationRequest(string Email, string Password);
    private record AuthenticationResponse(string Id, string UserName, string Email, string Token);
    private record RegistrationRequest(string FirstName, string LastName, string Email, string UserName, string Password);
    private record RegistrationResponse(string UserId);
}
