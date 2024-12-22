using BarberShopScheduler.Api.Dtos;
using BarberShopScheduler.Api.Models;

namespace BarberShopScheduler.Api.Interfaces
{
    public interface IPaymentService
    {
        /// <summary>
        /// Processa um pagamento com base nos detalhes fornecidos.
        /// </summary>
        /// <param name="request">Modelo contendo os detalhes do pagamento.</param>
        /// <returns>Resultado do processamento do pagamento.</returns>
        Task<PaymentResult> ProcessPaymentAsync(PaymentRequest request);

        /// <summary>
        /// Verifica o status de um pagamento existente.
        /// </summary>
        /// <param name="paymentId">Identificador do pagamento.</param>
        /// <returns>Detalhes do status do pagamento.</returns>
        Task<PaymentStatus> CheckPaymentStatusAsync(string paymentId);

        /// <summary>
        /// Lista os pagamentos realizados para uma barbearia específica.
        /// </summary>
        /// <param name="barberShopId">Identificador da barbearia.</param>
        /// <returns>Lista de pagamentos relacionados à barbearia.</returns>
        Task<List<Payment>> GetPaymentsByBarberShopAsync(Guid barberShopId);
    }
}
