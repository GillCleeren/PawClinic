namespace PawClinic.App.ViewModels;

public class AppointmentDetailViewModel
{
    public Guid AppointmentId { get; set; }
    public DateTime ScheduledDateTime { get; set; }
    public string ReasonForVisit { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string? Notes { get; set; }
    public Guid PetId { get; set; }
    public string PetName { get; set; } = string.Empty;
    public string PetSpecies { get; set; } = string.Empty;
    public Guid VetId { get; set; }
    public string VetName { get; set; } = string.Empty;
    public string VetSpecialisation { get; set; } = string.Empty;
}
