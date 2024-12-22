using BarberShopScheduler.Api.Interfaces;
using BarberShopScheduler.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace BarberShopScheduler.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentsController : ControllerBase
    {
        private readonly  IAppointmentService _appointmentService;

        public AppointmentsController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        // GET: api/appointments
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var appointments = await _appointmentService.GetAllAppointmentsAsync();
            return Ok(appointments);
        }

        // GET: api/appointments/{id}
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var appointment = await _appointmentService.GetAppointmentByIdAsync(id);
            if (appointment == null)
            {
                return NotFound(new { Message = $"Agendamento com o ID {id} não encontrado." });
            }
            return Ok(appointment);
        }

         // GET: api/appointments/{BarberShopId}
        [HttpGet("barbershop/{BarberShopId:guid}")]
        public async Task<IActionResult> GetByBarberShopId(Guid BarberShopId)
        {
            var appointment = await _appointmentService.GetAppointmentByBarberShopIdAsync(BarberShopId);
            if (appointment == null)
            {
                return NotFound(new { Message = $"Agendamentos com o BarberShopId {BarberShopId} não foram encontrados." });
            }
            return Ok(appointment);
        }

        // POST: api/appointments
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Appointment model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _appointmentService.CreateAppointmentAsync(model);
                return CreatedAtAction(nameof(GetById), new { id = model.Id }, model);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Erro ao criar agendamento.", Details = ex.Message });
            }
        }

        // PUT: api/appointments/{id}
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] Appointment model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var appointment = await _appointmentService.GetAppointmentByIdAsync(id);
            if (appointment == null)
            {
                return NotFound(new { Message = $"Agendamento com o ID {id} não encontrado." });
            }

            try
            {
                appointment.UpdateDetails(model.CustomerName, model.CustomerPhone, model.ServiceDescription);
                await _appointmentService.UpdateAppointmentAsync(id, appointment);
                return Ok(appointment);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Erro ao atualizar agendamento.", Details = ex.Message });
            }
        }

        // DELETE: api/appointments/{id}
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var appointment = await _appointmentService.GetAppointmentByIdAsync(id);
            if (appointment == null)
            {
                return NotFound(new { Message = $"Agendamento com o ID {id} não encontrado." });
            }

            try
            {
                await _appointmentService.DeleteAppointmentAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Erro ao deletar agendamento.", Details = ex.Message });
            }
        }
    }
}
