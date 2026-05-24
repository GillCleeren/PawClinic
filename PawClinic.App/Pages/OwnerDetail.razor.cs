using Microsoft.AspNetCore.Components;
using PawClinic.App.Contracts;
using PawClinic.App.ViewModels;

namespace PawClinic.App.Pages;

public partial class OwnerDetail
{
    [Parameter]
    public Guid OwnerId { get; set; }

    [Inject]
    public IOwnerDataService OwnerDataService { get; set; } = default!;

    [Inject]
    public NavigationManager NavigationManager { get; set; } = default!;

    public OwnerViewModel? Owner { get; set; }
    public List<PetViewModel> Pets { get; set; } = new();

    protected override async Task OnInitializedAsync()
    {
        Owner = await OwnerDataService.GetOwnerById(OwnerId);
    }
}
