using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;

namespace BarberShopScheduler.Web.Model;

public class CalendarModel : PageModel
{
    private readonly AppointmentService _appointmentService;

    public CalendarModel(AppointmentService appointmentService)
    {
        _appointmentService = appointmentService;
    }

    public Dictionary<string, List<Appointment>> Appointments { get; set; }
    public string BarberShopId { get; set; } = "5e51932c-b7e3-11ef-b363-a8a159004237"; // ID da barbearia

    // Método para carregar os agendamentos
    public async Task OnGetAsync()
    {
        // Carregar os agendamentos
         await _appointmentService.LoadAppointmentsAsync(BarberShopId);
    }

    // Método para salvar um novo agendamento
    public async Task<IActionResult> OnPostSaveAppointmentAsync(Appointment appointment)
    {
        try
        {
            // Salvar o agendamento
            await _appointmentService.SaveAppointmentAsync(appointment, BarberShopId);

            // Recarregar os agendamentos após salvar
            await _appointmentService.LoadAppointmentsAsync(BarberShopId);

            return RedirectToPage(); // Redireciona para a página do calendário com os dados atualizados
        }
        catch (Exception ex)
        {
            // Tratar erro
            ModelState.AddModelError(string.Empty, $"Erro ao salvar o agendamento: {ex.Message}");
            return Page();
        }
    }
}
