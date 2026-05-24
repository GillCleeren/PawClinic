using PawClinic.Domain.Entities;

namespace PawClinic.Application.Contracts.Persistence
{
    public interface IVetRepository : IAsyncRepository<Vet>
    {
    }
}
