using PawClinic.Domain.Common;
using PawClinic.Domain.Enums;

namespace PawClinic.Domain.Entities
{
    public class Appointment : AuditableEntity
    {
        public Guid AppointmentId { get; set; }
        public Guid PetId { get; set; }
        public Pet Pet { get; set; } = default!;
        public Guid VetId { get; set; }
        public Vet Vet { get; set; } = default!;
        public DateTime ScheduledDateTime { get; set; }
        public string ReasonForVisit { get; set; } = string.Empty;
        public AppointmentStatus Status { get; set; }
        public string? Notes { get; set; }
    }
}
