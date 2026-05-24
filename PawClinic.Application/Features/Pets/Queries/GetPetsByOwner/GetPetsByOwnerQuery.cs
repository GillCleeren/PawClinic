using MediatR;

namespace PawClinic.Application.Features.Pets.Queries.GetPetsByOwner
{
    public class GetPetsByOwnerQuery : IRequest<List<PetListVm>>
    {
        public Guid OwnerId { get; set; }
    }
}
