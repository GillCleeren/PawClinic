using MediatR;

namespace PawClinic.Application.Features.Appointments.Commands.CompleteAppointment
{
    public class CompleteAppointmentCommand : IRequest<Unit>
    {
        public Guid AppointmentId { get; set; }
        public string Notes { get; set; } = string.Empty;
    }
}
