using MediatR;

namespace PawClinic.Application.Features.Pets.Commands.ArchivePet
{
    public class ArchivePetCommand : IRequest<Unit>
    {
        public Guid PetId { get; set; }
    }
}
