using PawClinic.Domain.Entities;

namespace PawClinic.Application.Contracts.Persistence
{
    public interface IOwnerRepository : IAsyncRepository<Owner>
    {
        Task<bool> IsEmailUniqueAsync(string email);
    }
}
