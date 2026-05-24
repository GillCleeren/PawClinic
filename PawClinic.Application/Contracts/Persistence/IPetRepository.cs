using PawClinic.Domain.Entities;

namespace PawClinic.Application.Contracts.Persistence
{
    public interface IPetRepository : IAsyncRepository<Pet>
    {
        Task<List<Pet>> GetByOwnerAsync(Guid ownerId);
    }
}
