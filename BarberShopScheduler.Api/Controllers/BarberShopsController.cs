using BarberShopScheduler.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BarberShopScheduler.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BarberShopsController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public BarberShopsController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/barbershops
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var barberShops = await _dbContext.BarberShops.ToListAsync();
            return Ok(barberShops);
        }

        // GET: api/barbershops/{id}
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var barberShop = await _dbContext.BarberShops.FindAsync(id);
            if (barberShop == null)
            {
                return NotFound(new { Message = $"Barbearia com ID {id} não encontrada." });
            }
            return Ok(barberShop);
        }

        // POST: api/barbershops
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] BarberShop model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _dbContext.BarberShops.AddAsync(model);
                await _dbContext.SaveChangesAsync();
                return CreatedAtAction(nameof(GetById), new { id = model.Id }, model);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Erro ao criar barbearia.", Details = ex.Message });
            }
        }

        // PUT: api/barbershops/{id}
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] BarberShop model)
        {
            var barberShop = await _dbContext.BarberShops.FindAsync(id);
            if (barberShop == null)
            {
                return NotFound(new { Message = $"Barbearia com ID {id} não encontrada." });
            }

            try
            {
                barberShop.UpdateDetails(model.Name, model.Address, model.PhoneNumber, model.ThemeColor);
                _dbContext.BarberShops.Update(barberShop);
                await _dbContext.SaveChangesAsync();
                return Ok(barberShop);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Erro ao atualizar barbearia.", Details = ex.Message });
            }
        }

        // DELETE: api/barbershops/{id}
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var barberShop = await _dbContext.BarberShops.FindAsync(id);
            if (barberShop == null)
            {
                return NotFound(new { Message = $"Barbearia com ID {id} não encontrada." });
            }

            try
            {
                _dbContext.BarberShops.Remove(barberShop);
                await _dbContext.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Erro ao deletar barbearia.", Details = ex.Message });
            }
        }
    }
}
