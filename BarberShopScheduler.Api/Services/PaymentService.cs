using BarberShopScheduler.Api.Dtos;
using BarberShopScheduler.Api.Interfaces;
using BarberShopScheduler.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace BarberShopScheduler.Api.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly ApplicationDbContext _dbContext;

        public PaymentService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<PaymentResult> ProcessPaymentAsync(PaymentRequest request)
        {
            try
            {
                if (request.Amount <= 0)
                    return PaymentResult.Failed("O valor do pagamento deve ser maior que zero.");
                var payment = new Payment(
                    Guid.NewGuid().ToString(), // Geração de um identificador único para o pagamento
                    request.BarberShopId,
                    request.Amount,
                    request.PaymentMethod,
                    DateTime.UtcNow
                );

                // Simulação de processamento de pagamento (exemplo: API de gateway de pagamento externo)
                var transactionId = Guid.NewGuid().ToString();

                await _dbContext.Payments.AddAsync(payment);
                await _dbContext.SaveChangesAsync();

                return PaymentResult.Successful(transactionId, "Pagamento processado com sucesso.");
            }
            catch (Exception ex)
            {
                return PaymentResult.Failed($"Erro ao processar o pagamento: {ex.Message}");
            }
        }

        public async Task<PaymentStatus> CheckPaymentStatusAsync(string paymentId)
        {
            var payment = await _dbContext.Payments
                .FirstOrDefaultAsync(p => p.PaymentId == paymentId);

            if (payment == null)
                throw new KeyNotFoundException("Pagamento não encontrado.");

            // Exemplo de status fictício
            var paymentStatus = new PaymentStatus(
                paymentId,
                PaymentState.Completed, // Status simulado
                "Pagamento concluído com sucesso."
            );

            return paymentStatus;
        }

        public async Task<List<Payment>> GetPaymentsByBarberShopAsync(Guid barberShopId)
        {
            return await _dbContext.Payments
                .Where(p => p.BarberShopId == barberShopId)
                .ToListAsync();
        }
    }
}
