using BarberShopScheduler.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BarberShopScheduler.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentStatusController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public PaymentStatusController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/paymentstatus
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var statuses = await _dbContext.PaymentStatus.ToListAsync();
            return Ok(statuses);
        }

        // GET: api/paymentstatus/{id}
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var status = await _dbContext.PaymentStatus.FindAsync(id);
            if (status == null)
            {
                return NotFound(new { Message = $"Status de pagamento com ID {id} não encontrado." });
            }
            return Ok(status);
        }

        // POST: api/paymentstatus
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PaymentStatus model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _dbContext.PaymentStatus.AddAsync(model);
                await _dbContext.SaveChangesAsync();
                return CreatedAtAction(nameof(GetById), new { id = model.Id }, model);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Erro ao criar status de pagamento.", Details = ex.Message });
            }
        }

        // DELETE: api/paymentstatus/{id}
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var status = await _dbContext.PaymentStatus.FindAsync(id);
            if (status == null)
            {
                return NotFound(new { Message = $"Status de pagamento com ID {id} não encontrado." });
            }

            try
            {
                _dbContext.PaymentStatus.Remove(status);
                await _dbContext.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Erro ao deletar status de pagamento.", Details = ex.Message });
            }
        }
    }
}
