using PawClinic.Domain.Enums;

namespace PawClinic.Application.Features.Appointments.Queries.GetAppointmentById
{
    public class AppointmentDetailVm
    {
        public Guid AppointmentId { get; set; }
        public DateTime ScheduledDateTime { get; set; }
        public string ReasonForVisit { get; set; } = string.Empty;
        public AppointmentStatus Status { get; set; }
        public string? Notes { get; set; }
        public PetDto Pet { get; set; } = default!;
        public VetDto Vet { get; set; } = default!;
    }
}
