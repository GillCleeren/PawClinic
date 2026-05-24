using Microsoft.EntityFrameworkCore;
using PawClinic.Application.Contracts.Persistence;
using PawClinic.Domain.Entities;

namespace PawClinic.Persistence.Repositories
{
    public class OwnerRepository : BaseRepository<Owner>, IOwnerRepository
    {
        public OwnerRepository(PawClinicDbContext dbContext) : base(dbContext) { }

        public async Task<bool> IsEmailUniqueAsync(string email)
        {
            return !await _dbContext.Owners.AnyAsync(o => o.Email == email);
        }
    }
}
