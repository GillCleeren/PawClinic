using Microsoft.AspNetCore.Components;
using PawClinic.App.Contracts;
using PawClinic.App.ViewModels;

namespace PawClinic.App.Pages;

public partial class ScheduleAppointment
{
    [Inject]
    public IAppointmentDataService AppointmentDataService { get; set; } = default!;

    [Inject]
    public IOwnerDataService OwnerDataService { get; set; } = default!;

    [Inject]
    public IPetDataService PetDataService { get; set; } = default!;

    [Inject]
    public IVetDataService VetDataService { get; set; } = default!;

    [Inject]
    public NavigationManager NavigationManager { get; set; } = default!;

    public List<OwnerViewModel> Owners { get; set; } = new();
    public List<PetViewModel> Pets { get; set; } = new();
    public List<VetViewModel> Vets { get; set; } = new();

    public Guid SelectedOwnerId { get; set; } = Guid.Empty;
    public string SelectedPetId { get; set; } = string.Empty;
    public string SelectedVetId { get; set; } = string.Empty;
    public DateTime ScheduledDateTime { get; set; } = DateTime.Now.AddDays(1).Date.AddHours(9);
    public string ReasonForVisit { get; set; } = string.Empty;
    public string ErrorMessage { get; set; } = string.Empty;

    protected override async Task OnInitializedAsync()
    {
        var ownersTask = OwnerDataService.GetAllOwners(size: 200);
        var vetsTask = VetDataService.GetAllVets();
        await Task.WhenAll(ownersTask, vetsTask);
        Owners = ownersTask.Result;
        Vets = vetsTask.Result;
    }

    public async Task OnOwnerChanged(ChangeEventArgs e)
    {
        if (Guid.TryParse(e.Value?.ToString(), out var ownerId) && ownerId != Guid.Empty)
        {
            SelectedOwnerId = ownerId;
            SelectedPetId = string.Empty;
            Pets = await PetDataService.GetPetsByOwner(ownerId);
        }
        else
        {
            SelectedOwnerId = Guid.Empty;
            SelectedPetId = string.Empty;
            Pets = new();
        }
    }

    public async Task HandleSubmit()
    {
        ErrorMessage = string.Empty;

        if (!Guid.TryParse(SelectedPetId, out var petId) || petId == Guid.Empty)
        {
            ErrorMessage = "Please select a pet.";
            return;
        }
        if (!Guid.TryParse(SelectedVetId, out var vetId) || vetId == Guid.Empty)
        {
            ErrorMessage = "Please select a vet.";
            return;
        }
        if (string.IsNullOrWhiteSpace(ReasonForVisit))
        {
            ErrorMessage = "Please enter a reason for the visit.";
            return;
        }
        if (ScheduledDateTime <= DateTime.Now)
        {
            ErrorMessage = "Scheduled date and time must be in the future.";
            return;
        }

        var result = await AppointmentDataService.ScheduleAppointment(petId, vetId, ScheduledDateTime, ReasonForVisit);
        if (result.Success)
            NavigationManager.NavigateTo("/appointments");
        else
            ErrorMessage = result.Message;
    }
}
