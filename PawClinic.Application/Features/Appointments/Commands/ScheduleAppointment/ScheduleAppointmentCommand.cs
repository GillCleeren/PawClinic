using MediatR;

namespace PawClinic.Application.Features.Appointments.Commands.ScheduleAppointment
{
    public class ScheduleAppointmentCommand : IRequest<Guid>
    {
        public Guid PetId { get; set; }
        public Guid VetId { get; set; }
        public DateTime ScheduledDateTime { get; set; }
        public string ReasonForVisit { get; set; } = string.Empty;
    }
}
