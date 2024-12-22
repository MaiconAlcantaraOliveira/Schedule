using BarberShopScheduler.Web.Model;
using System.Globalization;
using System.Text;
using System.Text.Json;


public class AppointmentService
{
    private readonly HttpClient _httpClient;
    private Dictionary<string, List<Appointment>> _appointments = new();

    public AppointmentService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    // Carregar agendamentos do backend
    public async Task LoadAppointmentsAsync(string barberShopId)
    {
        try
        {
            var response = await _httpClient.GetAsync($"https://localhost:7035/api/Appointments/barbershop/{barberShopId}");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var appointments = JsonSerializer.Deserialize<List<Appointment>>(json);

            _appointments = appointments?
                .GroupBy(a => a.Date.ToString("yyyy-MM-dd"))
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(a => new Appointment
                    {
                        Date = a.Date,
                        StartTime = a.StartTime.Substring(0, 5),
                        EndTime = a.EndTime.Substring(0, 5),
                        CustomerName = a.CustomerName,
                        CustomerPhone = a.CustomerPhone,
                        ServiceDescription = a.ServiceDescription
                    }).ToList()
                ) ?? new Dictionary<string, List<Appointment>>();

            Console.WriteLine("Agendamentos processados:");
            foreach (var date in _appointments.Keys)
            {
                Console.WriteLine($"Data: {date}, Agendamentos: {_appointments[date].Count}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao carregar agendamentos: {ex.Message}");
        }
    }

    // Salvar agendamento no backend
    public async Task SaveAppointmentAsync(Appointment appointment, string barberShopId)
    {
        try
        {
            var content = new StringContent(JsonSerializer.Serialize(appointment), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("https://localhost:7035/api/Appointments", content);
            response.EnsureSuccessStatusCode();

            Console.WriteLine("Agendamento salvo com sucesso!");
            await LoadAppointmentsAsync(barberShopId); // Atualiza os agendamentos depois de salvar
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao salvar agendamento: {ex.Message}");
        }
    }

    // Renderizar calendário
    public void RenderCalendar(DateTime date, int barberShopId)
    {
        var year = date.Year;
        var month = date.Month;
        var firstDay = new DateTime(year, month, 1).DayOfWeek;
        var daysInMonth = DateTime.DaysInMonth(year, month);
        var today = DateTime.Today;
        var yesterday = today.AddDays(-1);

        Console.WriteLine($"Renderizando calendário para {date.ToString("MMMM yyyy", CultureInfo.CurrentCulture)}");
        Console.WriteLine("DOM SEG TER QUA QUI SEX SAB");

        int dayCount = 1;
        for (int i = 0; i < 6; i++)
        {
            var weekRow = new List<string>();
            for (int j = 0; j < 7; j++)
            {
                if (i == 0 && (int)firstDay > j || dayCount > daysInMonth)
                {
                    weekRow.Add("   ");
                    continue;
                }

                var cellDate = new DateTime(year, month, dayCount);
                var dateKey = cellDate.ToString("yyyy-MM-dd");

                if (cellDate < yesterday)
                {
                    weekRow.Add(" x ");
                }
                else if (_appointments.ContainsKey(dateKey))
                {
                    weekRow.Add($"[{dayCount}]");
                }
                else
                {
                    weekRow.Add($" {dayCount} ");
                }

                dayCount++;
            }
            Console.WriteLine(string.Join(" ", weekRow));
        }
    }
}
