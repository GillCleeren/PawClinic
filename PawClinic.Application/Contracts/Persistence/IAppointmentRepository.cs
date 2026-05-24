using PawClinic.Domain.Entities;

namespace PawClinic.Application.Contracts.Persistence
{
    public interface IAppointmentRepository : IAsyncRepository<Appointment>
    {
        Task<bool> IsVetDoubleBookedAsync(Guid vetId, DateTime slot, Guid? excludeAppointmentId = null);
        Task<bool> IsPetDoubleBookedAsync(Guid petId, DateTime slot, Guid? excludeAppointmentId = null);
        Task<List<Appointment>> GetUpcomingAsync(Guid? vetId, Guid? petId);
        Task<List<Appointment>> GetHistoryAsync(Guid petId);
        Task<List<Appointment>> GetVetScheduleAsync(Guid vetId, DateOnly date);
    }
}
