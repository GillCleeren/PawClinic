using PawClinic.Domain.Enums;

namespace PawClinic.Application.Features.Appointments.Queries.GetAppointmentById
{
    public class VetDto
    {
        public Guid VetId { get; set; }
        public string Name { get; set; } = string.Empty;
        public VetSpecialisation Specialisation { get; set; }
    }
}
