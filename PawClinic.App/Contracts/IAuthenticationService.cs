using PawClinic.App.ViewModels;

namespace PawClinic.App.Contracts;

public interface IAuthenticationService
{
    Task<bool> Login(LoginViewModel loginViewModel);
    Task<bool> Register(RegisterViewModel registerViewModel);
    Task Logout();
}
