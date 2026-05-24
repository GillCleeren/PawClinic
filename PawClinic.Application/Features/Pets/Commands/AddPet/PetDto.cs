using PawClinic.Domain.Enums;

namespace PawClinic.Application.Features.Pets.Commands.AddPet
{
    public class PetDto
    {
        public Guid PetId { get; set; }
        public string Name { get; set; } = string.Empty;
        public Species Species { get; set; }
        public string? Breed { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Guid OwnerId { get; set; }
    }
}
