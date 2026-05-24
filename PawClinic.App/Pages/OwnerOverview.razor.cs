using Microsoft.AspNetCore.Components;
using PawClinic.App.Contracts;
using PawClinic.App.ViewModels;

namespace PawClinic.App.Pages;

public partial class OwnerOverview
{
    [Inject]
    public IOwnerDataService OwnerDataService { get; set; } = default!;

    [Inject]
    public NavigationManager NavigationManager { get; set; } = default!;

    public List<OwnerViewModel>? Owners { get; set; }

    protected override async Task OnInitializedAsync()
    {
        Owners = await OwnerDataService.GetAllOwners();
    }
}
