using MediatR;

namespace PawClinic.Application.Features.Pets.Queries.GetPetById
{
    public class GetPetByIdQuery : IRequest<PetDetailVm>
    {
        public Guid PetId { get; set; }
    }
}
