using MediatR;

namespace PawClinic.Application.Features.Owners.Queries.GetOwnerById
{
    public class GetOwnerByIdQuery : IRequest<OwnerDetailVm>
    {
        public Guid OwnerId { get; set; }
    }
}
