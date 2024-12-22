using BarberShopScheduler.Api.Interfaces;
using BarberShopScheduler.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace BarberShopScheduler.Api.Services
{
    public class BarberShopService : IBarberShopService
    {
        private readonly ApplicationDbContext _dbContext;

        // Injeção de dependência do DbContext
        public BarberShopService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<List<BarberShop>> GetAllBarberShopsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<BarberShop?> GetBarberShopByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        // Método para registrar uma barbearia
        public async Task<ServiceResult> RegisterBarberShopAsync(BarberShopRegister model)
        {
            try
            {
                // Valida se a barbearia já existe
                var existingBarberShop = await _dbContext.BarberShops
                    .FirstOrDefaultAsync(b => b.Name.Trim().Equals(model.Name.Trim(), StringComparison.OrdinalIgnoreCase));

                if (existingBarberShop != null)
                {
                    return ServiceResult.Failure("Barbearia já registrada com esse nome.");
                }

                // Cria a nova barbearia
                var barberShop = new BarberShop(
                    model.Name.Trim(),
                    model.OwnerName.Trim(),
                    model.Address.Trim(),
                    model.PhoneNumber.Trim(),
                    model.Email.Trim(),
                    null
                );

                // Adiciona e salva no banco
                await _dbContext.BarberShops.AddAsync(barberShop);
                await _dbContext.SaveChangesAsync();

                return ServiceResult.Successful("Barbearia registrada com sucesso.");
            }
            catch (Exception ex)
            {
                return ServiceResult.Failure($"Erro ao registrar a barbearia: {ex.Message}");
            }
        }

        // Método para atualizar os dados de uma barbearia
        public async Task<ServiceResult> UpdateBarberShopAsync(Guid id, BarberShopUpdate model)
        {
            try
            {
                // Busca a barbearia pelo ID
                var barberShop = await _dbContext.BarberShops.FindAsync(id);
                if (barberShop == null)
                {
                    return ServiceResult.Failure("Barbearia não encontrada.");
                }

                // Atualiza os dados fornecidos
                barberShop.UpdateDetails(
                    model.Name?.Trim(),
                    model.Address?.Trim(),
                    model.PhoneNumber?.Trim(),
                    string.Empty
                );

                // Salva as alterações no banco
                _dbContext.BarberShops.Update(barberShop);
                await _dbContext.SaveChangesAsync();

                return ServiceResult.Successful("Barbearia atualizada com sucesso.");
            }
            catch (Exception ex)
            {
                return ServiceResult.Failure($"Erro ao atualizar a barbearia: {ex.Message}");
            }
        }
    }
}
