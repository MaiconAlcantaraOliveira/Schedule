using BarberShopScheduler.Api.Models;

namespace BarberShopScheduler.Api.Interfaces
{
    public interface IBarberShopService
    {
        /// <summary>
        /// Registra uma nova barbearia.
        /// </summary>
        /// <param name="model">Modelo contendo informações da barbearia a ser registrada.</param>
        /// <returns>Resultado do registro contendo status e mensagem.</returns>
        Task<ServiceResult> RegisterBarberShopAsync(BarberShopRegister model);

        /// <summary>
        /// Atualiza as informações de uma barbearia existente.
        /// </summary>
        /// <param name="id">Identificador da barbearia.</param>
        /// <param name="model">Modelo contendo as informações a serem atualizadas.</param>
        /// <returns>Resultado da atualização contendo status e mensagem.</returns>
        Task<ServiceResult> UpdateBarberShopAsync(Guid id, BarberShopUpdate model);

        /// <summary>
        /// Obtém os detalhes de uma barbearia.
        /// </summary>
        /// <param name="id">Identificador da barbearia.</param>
        /// <returns>Detalhes da barbearia.</returns>
        Task<BarberShop?> GetBarberShopByIdAsync(Guid id);

        /// <summary>
        /// Lista todas as barbearias registradas.
        /// </summary>
        /// <returns>Lista de barbearias.</returns>
        Task<List<BarberShop>> GetAllBarberShopsAsync();
    }
}
