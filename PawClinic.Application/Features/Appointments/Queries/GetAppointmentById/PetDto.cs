using PawClinic.Domain.Enums;

namespace PawClinic.Application.Features.Appointments.Queries.GetAppointmentById
{
    public class PetDto
    {
        public Guid PetId { get; set; }
        public string Name { get; set; } = string.Empty;
        public Species Species { get; set; }
    }
}
