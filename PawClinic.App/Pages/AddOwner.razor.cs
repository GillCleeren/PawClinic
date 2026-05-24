using Microsoft.AspNetCore.Components;
using PawClinic.App.Contracts;
using PawClinic.App.ViewModels;

namespace PawClinic.App.Pages;

public partial class AddOwner
{
    [Inject]
    public IOwnerDataService OwnerDataService { get; set; } = default!;

    [Inject]
    public NavigationManager NavigationManager { get; set; } = default!;

    public OwnerViewModel OwnerViewModel { get; set; } = new();
    public string Message { get; set; } = string.Empty;

    protected async void HandleValidSubmit()
    {
        var result = await OwnerDataService.RegisterOwner(OwnerViewModel);
        if (result.Success)
        {
            NavigationManager.NavigateTo("/owners");
        }
        else
        {
            Message = result.Message;
        }
    }
}
