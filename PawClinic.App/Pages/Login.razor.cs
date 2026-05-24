using Microsoft.AspNetCore.Components;
using PawClinic.App.Contracts;
using PawClinic.App.ViewModels;

namespace PawClinic.App.Pages;

public partial class Login
{
    [Inject]
    public IAuthenticationService AuthenticationService { get; set; } = default!;

    [Inject]
    public NavigationManager NavigationManager { get; set; } = default!;

    public LoginViewModel LoginViewModel { get; set; } = new();
    public string Message { get; set; } = string.Empty;

    protected override void OnInitialized()
    {
        LoginViewModel = new LoginViewModel();
    }

    protected async void HandleValidSubmit()
    {
        if (await AuthenticationService.Login(LoginViewModel))
        {
            NavigationManager.NavigateTo("/");
        }
        else
        {
            Message = "Email/password combination unknown.";
        }
    }
}
