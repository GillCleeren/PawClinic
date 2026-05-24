using MediatR;

namespace PawClinic.Application.Features.Vets.Queries.GetAllVets
{
    public class GetAllVetsQuery : IRequest<List<VetListVm>>
    {
    }
}
