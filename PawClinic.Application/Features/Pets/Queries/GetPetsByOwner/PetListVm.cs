using PawClinic.Domain.Enums;

namespace PawClinic.Application.Features.Pets.Queries.GetPetsByOwner
{
    public class PetListVm
    {
        public Guid PetId { get; set; }
        public string Name { get; set; } = string.Empty;
        public Species Species { get; set; }
        public string? Breed { get; set; }
        public bool IsArchived { get; set; }
    }
}
