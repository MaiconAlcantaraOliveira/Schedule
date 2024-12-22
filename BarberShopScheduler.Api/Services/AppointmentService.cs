using BarberShopScheduler.Api.Interfaces;
using BarberShopScheduler.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace BarberShopScheduler.Api.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly ApplicationDbContext _dbContext;

        public AppointmentService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Recupera todos os agendamentos
        public async Task<List<Appointment>> GetAllAppointmentsAsync()
        {
            return await _dbContext.Appointments
                .OrderBy(a => a.StartTime)
                .ToListAsync();
        }

        // Recupera um agendamento pelo ID
        public async Task<Appointment?> GetAppointmentByIdAsync(Guid id)
        {
            return await _dbContext.Appointments
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        // Recupera um agendamento pelo BarberShopID
        public async Task<IEnumerable<Appointment>> GetAppointmentByBarberShopIdAsync(Guid id)
        {
            return await _dbContext.Appointments
                .OrderBy(a => a.StartTime)
                .Where(a => a.BarberShopId == id && a.Date >= DateTime.Now)
                .ToListAsync();
        }

        // Cria um novo agendamento
        public async Task<ServiceResult> CreateAppointmentAsync(Appointment appointment)
        {
            try
            {
                // Verifica se há conflitos no horário
                var conflict = await _dbContext.Appointments.AnyAsync(a =>
                    a.BarberShopId == appointment.BarberShopId &&
                    a.Date == appointment.Date &&
                    ((appointment.StartTime >= a.StartTime && appointment.StartTime < a.EndTime) ||
                     (appointment.EndTime > a.StartTime && appointment.EndTime <= a.EndTime)));

                var timeOnly = TimeOnly.FromTimeSpan(appointment.StartTime);
                var dateOnly = DateOnly.FromDateTime(appointment.Date); 
                appointment.Date = new DateTime(dateOnly, timeOnly);


                if (conflict)
                {
                    return ServiceResult.Failure("O horário do agendamento está em conflito com outro já existente.");
                }

                // Adiciona e salva o agendamento
                await _dbContext.Appointments.AddAsync(appointment);
                await _dbContext.SaveChangesAsync();

                return ServiceResult.Successful("Agendamento criado com sucesso.");
            }
            catch (Exception ex)
            {
                return ServiceResult.Failure($"Erro ao criar o agendamento: {ex.Message}");
            }
        }

        // Atualiza um agendamento existente
        public async Task<ServiceResult> UpdateAppointmentAsync(Guid id, Appointment updatedAppointment)
        {
            try
            {
                var appointment = await _dbContext.Appointments.FindAsync(id);
                if (appointment == null)
                {
                    return ServiceResult.Failure("Agendamento não encontrado.");
                }

                // Atualiza os dados do agendamento
                appointment.UpdateDetails(
                    updatedAppointment.CustomerName,
                    updatedAppointment.CustomerPhone,
                    updatedAppointment.ServiceDescription
                );

                appointment.Date = updatedAppointment.Date;
                appointment.StartTime = updatedAppointment.StartTime;
                appointment.EndTime = updatedAppointment.EndTime;
                appointment.UpdatedAt = DateTime.UtcNow;

                _dbContext.Appointments.Update(appointment);
                await _dbContext.SaveChangesAsync();

                return ServiceResult.Successful("Agendamento atualizado com sucesso.");
            }
            catch (Exception ex)
            {
                return ServiceResult.Failure($"Erro ao atualizar o agendamento: {ex.Message}");
            }
        }

        // Exclui um agendamento
        public async Task<ServiceResult> DeleteAppointmentAsync(Guid id)
        {
            try
            {
                var appointment = await _dbContext.Appointments.FindAsync(id);
                if (appointment == null)
                {
                    return ServiceResult.Failure("Agendamento não encontrado.");
                }

                _dbContext.Appointments.Remove(appointment);
                await _dbContext.SaveChangesAsync();

                return ServiceResult.Successful("Agendamento excluído com sucesso.");
            }
            catch (Exception ex)
            {
                return ServiceResult.Failure($"Erro ao excluir o agendamento: {ex.Message}");
            }
        }
    }
}
