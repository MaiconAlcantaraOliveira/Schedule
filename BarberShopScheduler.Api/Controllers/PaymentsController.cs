using BarberShopScheduler.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BarberShopScheduler.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentsController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public PaymentsController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/payments
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var payments = await _dbContext.Payments.ToListAsync();
            return Ok(payments);
        }

        // GET: api/payments/{id}
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var payment = await _dbContext.Payments.FindAsync(id);
            if (payment == null)
            {
                return NotFound(new { Message = $"Pagamento com ID {id} não encontrado." });
            }
            return Ok(payment);
        }

        // POST: api/payments
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Payment model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _dbContext.Payments.AddAsync(model);
                await _dbContext.SaveChangesAsync();
                return CreatedAtAction(nameof(GetById), new { id = model.Id }, model);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Erro ao criar pagamento.", Details = ex.Message });
            }
        }

        // DELETE: api/payments/{id}
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var payment = await _dbContext.Payments.FindAsync(id);
            if (payment == null)
            {
                return NotFound(new { Message = $"Pagamento com ID {id} não encontrado." });
            }

            try
            {
                _dbContext.Payments.Remove(payment);
                await _dbContext.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Erro ao deletar pagamento.", Details = ex.Message });
            }
        }
    }
}
