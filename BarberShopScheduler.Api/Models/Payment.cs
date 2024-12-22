using System;
using System.ComponentModel.DataAnnotations;

namespace BarberShopScheduler.Api.Models
{
    public class Payment
    {
        [Key]
        public Guid Id { get; set; } // Chave primária gerada pelo EF Core

        [Required(ErrorMessage = "O identificador do pagamento é obrigatório.")]
        [StringLength(50, ErrorMessage = "O identificador do pagamento deve ter até 50 caracteres.")]
        public string PaymentId { get; set; } // Identificador único do pagamento (do tipo string)

        [Required]
        public Guid BarberShopId { get; set; } // Relacionamento com BarberShop

        [Required(ErrorMessage = "O valor do pagamento é obrigatório.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O valor do pagamento deve ser maior que zero.")]
        public decimal Amount { get; set; } // Valor do pagamento

        [Required(ErrorMessage = "O método de pagamento é obrigatório.")]
        public PaymentMethod PaymentMethod { get; set; } // Método de pagamento (Usando enum)

        [Required(ErrorMessage = "A data do pagamento é obrigatória.")]
        [DataType(DataType.DateTime, ErrorMessage = "A data do pagamento deve ser uma data válida.")]
        public DateTime Date { get; set; } // Data do pagamento

        // Relacionamento com BarberShop
        public BarberShop BarberShop { get; set; }

        private Payment() { } // Construtor padrão para EF Core

        public Payment(string paymentId, Guid barberShopId, decimal amount, PaymentMethod paymentMethod, DateTime? date = null)
        {
            PaymentId = paymentId;
            BarberShopId = barberShopId;
            Amount = amount;
            PaymentMethod = paymentMethod;
            Date = date ?? DateTime.UtcNow;
        }

        public void UpdateDate(DateTime date)
        {
            if (date > DateTime.UtcNow)
                throw new ArgumentException("A data do pagamento não pode ser no futuro.", nameof(date));

            Date = date;
        }
    }

    // Enum para métodos de pagamento
    public enum PaymentMethod
    {
        Cartao,   // Cartão
        Pix,      // Pix
        Dinheiro, // Dinheiro
        Boleto    // Boleto
    }
}
