using PawClinic.Application.Contracts.Persistence;
using PawClinic.Domain.Entities;

namespace PawClinic.Persistence.Repositories
{
    public class VetRepository : BaseRepository<Vet>, IVetRepository
    {
        public VetRepository(PawClinicDbContext dbContext) : base(dbContext) { }
    }
}
