using MediatR;

namespace PawClinic.Application.Features.Appointments.Commands.CancelAppointment
{
    public class CancelAppointmentCommand : IRequest<Unit>
    {
        public Guid AppointmentId { get; set; }
        public string? Reason { get; set; }
    }
}
