using BarberShopScheduler.Api.Models;

namespace BarberShopScheduler.Api.Dtos
{
    public class PaymentRequest
    {
        public Guid BarberShopId { get; private set; }
        public decimal Amount { get; private set; }
        public PaymentMethod PaymentMethod { get; private set; }

        public PaymentRequest(Guid barberShopId, decimal amount, PaymentMethod paymentMethod)
        {
            if (amount <= 0)
                throw new ArgumentException("O valor do pagamento deve ser maior que zero.", nameof(amount));
            if (string.IsNullOrWhiteSpace(paymentMethod.ToString()))
                throw new ArgumentException("O método de pagamento é obrigatório.", nameof(paymentMethod));

            BarberShopId = barberShopId;
            Amount = amount;
            PaymentMethod = paymentMethod;
        }
    }

}
