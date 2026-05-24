using MediatR;

namespace PawClinic.Application.Features.Owners.Queries.GetAllOwners
{
    public class GetAllOwnersQuery : IRequest<PagedOwnerListVm>
    {
        public int Page { get; set; } = 1;
        public int Size { get; set; } = 10;
    }
}
