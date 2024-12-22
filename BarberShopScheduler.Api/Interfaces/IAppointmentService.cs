using BarberShopScheduler.Api.Models;

namespace BarberShopScheduler.Api.Interfaces
{
    public interface IAppointmentService
    {
        Task<List<Appointment>> GetAllAppointmentsAsync();
        Task<Appointment?> GetAppointmentByIdAsync(Guid id);
        Task<IEnumerable<Appointment>> GetAppointmentByBarberShopIdAsync(Guid id);
        Task<ServiceResult> CreateAppointmentAsync(Appointment appointment);
        Task<ServiceResult> UpdateAppointmentAsync(Guid id, Appointment updatedAppointment);
        Task<ServiceResult> DeleteAppointmentAsync(Guid id);
    }
}
