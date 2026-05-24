using MediatR;
using PawClinic.Domain.Enums;

namespace PawClinic.Application.Features.Pets.Commands.AddPet
{
    public class AddPetCommand : IRequest<AddPetCommandResponse>
    {
        public string Name { get; set; } = string.Empty;
        public Species Species { get; set; }
        public string? Breed { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Guid OwnerId { get; set; }
    }
}
