using Microsoft.EntityFrameworkCore;
using PawClinic.Application.Contracts.Persistence;
using PawClinic.Domain.Entities;

namespace PawClinic.Persistence.Repositories
{
    public class PetRepository : BaseRepository<Pet>, IPetRepository
    {
        public PetRepository(PawClinicDbContext dbContext) : base(dbContext) { }

        public async Task<List<Pet>> GetByOwnerAsync(Guid ownerId)
        {
            return await _dbContext.Pets
                .Where(p => p.OwnerId == ownerId)
                .ToListAsync();
        }
    }
}
