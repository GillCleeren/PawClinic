using Microsoft.EntityFrameworkCore;
using PawClinic.Application.Contracts.Persistence;
using PawClinic.Domain.Entities;
using PawClinic.Domain.Enums;

namespace PawClinic.Persistence.Repositories
{
    public class AppointmentRepository : BaseRepository<Appointment>, IAppointmentRepository
    {
        public AppointmentRepository(PawClinicDbContext dbContext) : base(dbContext) { }

        public async Task<bool> IsVetDoubleBookedAsync(Guid vetId, DateTime slot, Guid? excludeAppointmentId = null)
        {
            return await _dbContext.Appointments
                .Where(a => a.VetId == vetId
                    && a.Status == AppointmentStatus.Scheduled
                    && a.ScheduledDateTime == slot
                    && (excludeAppointmentId == null || a.AppointmentId != excludeAppointmentId))
                .AnyAsync();
        }

        public async Task<bool> IsPetDoubleBookedAsync(Guid petId, DateTime slot, Guid? excludeAppointmentId = null)
        {
            return await _dbContext.Appointments
                .Where(a => a.PetId == petId
                    && a.Status == AppointmentStatus.Scheduled
                    && a.ScheduledDateTime == slot
                    && (excludeAppointmentId == null || a.AppointmentId != excludeAppointmentId))
                .AnyAsync();
        }

        public async Task<List<Appointment>> GetUpcomingAsync(Guid? vetId, Guid? petId)
        {
            var query = _dbContext.Appointments
                .Include(a => a.Pet)
                .Include(a => a.Vet)
                .Where(a => a.Status == AppointmentStatus.Scheduled && a.ScheduledDateTime > DateTime.UtcNow);

            if (vetId.HasValue)
                query = query.Where(a => a.VetId == vetId.Value);

            if (petId.HasValue)
                query = query.Where(a => a.PetId == petId.Value);

            return await query.OrderBy(a => a.ScheduledDateTime).ToListAsync();
        }

        public async Task<List<Appointment>> GetHistoryAsync(Guid petId)
        {
            return await _dbContext.Appointments
                .Include(a => a.Pet)
                .Include(a => a.Vet)
                .Where(a => a.PetId == petId && a.Status != AppointmentStatus.Scheduled)
                .OrderByDescending(a => a.ScheduledDateTime)
                .ToListAsync();
        }

        public async Task<List<Appointment>> GetVetScheduleAsync(Guid vetId, DateOnly date)
        {
            return await _dbContext.Appointments
                .Include(a => a.Pet)
                .Include(a => a.Vet)
                .Where(a => a.VetId == vetId
                    && DateOnly.FromDateTime(a.ScheduledDateTime) == date)
                .OrderBy(a => a.ScheduledDateTime)
                .ToListAsync();
        }
    }
}
