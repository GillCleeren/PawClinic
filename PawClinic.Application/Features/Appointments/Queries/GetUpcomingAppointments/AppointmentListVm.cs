using PawClinic.Domain.Enums;

namespace PawClinic.Application.Features.Appointments.Queries.GetUpcomingAppointments
{
    public class AppointmentListVm
    {
        public Guid AppointmentId { get; set; }
        public DateTime ScheduledDateTime { get; set; }
        public string ReasonForVisit { get; set; } = string.Empty;
        public AppointmentStatus Status { get; set; }
        public string PetName { get; set; } = string.Empty;
        public string VetName { get; set; } = string.Empty;
    }
}
