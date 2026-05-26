using Microsoft.AspNetCore.Components;
using PawClinic.App.Contracts;
using PawClinic.App.ViewModels;

namespace PawClinic.App.Pages;

public partial class AddPet
{
    [Parameter]
    public Guid OwnerId { get; set; }

    [Inject]
    public IPetDataService PetDataService { get; set; } = default!;

    [Inject]
    public NavigationManager NavigationManager { get; set; } = default!;

    public PetViewModel Pet { get; set; } = new() { DateOfBirth = DateTime.Today };
    public int SelectedSpecies { get; set; } = 0;
    public string Message { get; set; } = string.Empty;

    protected override void OnParametersSet()
    {
        Pet.OwnerId = OwnerId;
    }

    protected async void HandleValidSubmit()
    {
        var result = await PetDataService.AddPet(Pet, SelectedSpecies);
        if (result.Success)
        {
            NavigationManager.NavigateTo($"/owners/{OwnerId}");
        }
        else
        {
            Message = result.Message;
        }
    }
}
