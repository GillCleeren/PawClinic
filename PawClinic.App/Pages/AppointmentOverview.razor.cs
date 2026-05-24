using Microsoft.AspNetCore.Components;
using PawClinic.App.Contracts;
using PawClinic.App.ViewModels;

namespace PawClinic.App.Pages;

public partial class AppointmentOverview
{
    [Inject]
    public IAppointmentDataService AppointmentDataService { get; set; } = default!;

    [Inject]
    public NavigationManager NavigationManager { get; set; } = default!;

    public List<AppointmentListViewModel>? Appointments { get; set; }

    protected override async Task OnInitializedAsync()
    {
        Appointments = await AppointmentDataService.GetUpcomingAppointments();
    }

    public string GetStatusClass(string? status) => (status ?? "").ToLower() switch {
        "scheduled"   => "badge-scheduled",
        "completed"   => "badge-completed",
        "cancelled"   => "badge-cancelled",
        "in progress" => "badge-inprogress",
        "inprogress"  => "badge-inprogress",
        _             => "badge-scheduled"
    };
}
