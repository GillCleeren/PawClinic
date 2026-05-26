using Microsoft.AspNetCore.Components;
using PawClinic.App.Contracts;
using PawClinic.App.ViewModels;

namespace PawClinic.App.Pages;

public partial class AppointmentDetail
{
    [Parameter]
    public Guid AppointmentId { get; set; }

    [Inject]
    public IAppointmentDataService AppointmentDataService { get; set; } = default!;

    [Inject]
    public NavigationManager NavigationManager { get; set; } = default!;

    public AppointmentDetailViewModel? Appointment { get; set; }
    public bool ShowCompleteForm { get; set; }
    public bool ShowCancelForm { get; set; }
    public string CompletionNotes { get; set; } = string.Empty;
    public string CancelReason { get; set; } = string.Empty;
    public string ErrorMessage { get; set; } = string.Empty;

    protected override async Task OnInitializedAsync()
    {
        Appointment = await AppointmentDataService.GetAppointmentById(AppointmentId);
    }

    public async Task HandleComplete()
    {
        var result = await AppointmentDataService.CompleteAppointment(AppointmentId, CompletionNotes);
        if (result.Success)
            NavigationManager.NavigateTo("/appointments");
        else
            ErrorMessage = result.Message;
    }

    public async Task HandleCancel()
    {
        var result = await AppointmentDataService.CancelAppointment(AppointmentId, CancelReason);
        if (result.Success)
            NavigationManager.NavigateTo("/appointments");
        else
            ErrorMessage = result.Message;
    }

    public string GetStatusClass(string? status) => (status ?? "").ToLower() switch
    {
        "scheduled"  => "badge-scheduled",
        "completed"  => "badge-completed",
        "cancelled"  => "badge-cancelled",
        _            => "badge-scheduled"
    };
}
