using PawClinic.Domain.Enums;

namespace PawClinic.Application.Features.Vets.Queries.GetAllVets
{
    public class VetListVm
    {
        public Guid VetId { get; set; }
        public string Name { get; set; } = string.Empty;
        public VetSpecialisation Specialisation { get; set; }
    }
}
