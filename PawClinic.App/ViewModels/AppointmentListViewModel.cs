namespace PawClinic.App.ViewModels;

public class AppointmentListViewModel
{
    public Guid AppointmentId { get; set; }
    public DateTime ScheduledDateTime { get; set; }
    public string ReasonForVisit { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string PetName { get; set; } = string.Empty;
    public string VetName { get; set; } = string.Empty;
}
