using Microsoft.AspNetCore.Components;
using PawClinic.App.Contracts;
using PawClinic.App.ViewModels;

namespace PawClinic.App.Pages;

public partial class Register
{
    [Inject]
    public IAuthenticationService AuthenticationService { get; set; } = default!;

    [Inject]
    public NavigationManager NavigationManager { get; set; } = default!;

    public RegisterViewModel RegisterViewModel { get; set; } = new();
    public string Message { get; set; } = string.Empty;

    protected override void OnInitialized()
    {
        RegisterViewModel = new RegisterViewModel();
    }

    protected async void HandleValidSubmit()
    {
        if (await AuthenticationService.Register(RegisterViewModel))
        {
            NavigationManager.NavigateTo("/login");
        }
        else
        {
            Message = "Registration failed. Please check your details and try again.";
        }
    }
}
