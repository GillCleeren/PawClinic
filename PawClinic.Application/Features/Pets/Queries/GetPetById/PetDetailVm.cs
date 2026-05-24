using PawClinic.Domain.Enums;

namespace PawClinic.Application.Features.Pets.Queries.GetPetById
{
    public class PetDetailVm
    {
        public Guid PetId { get; set; }
        public string Name { get; set; } = string.Empty;
        public Species Species { get; set; }
        public string? Breed { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool IsArchived { get; set; }
        public Guid OwnerId { get; set; }
        public string OwnerName { get; set; } = string.Empty;
    }
}
