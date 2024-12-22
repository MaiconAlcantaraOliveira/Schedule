using System;
using System.ComponentModel.DataAnnotations;

namespace BarberShopScheduler.Api.Models
{
    public class PaymentStatus
    {
        [Required(ErrorMessage = "O ID do pagamento é obrigatório.")]
        [StringLength(50, ErrorMessage = "O ID do pagamento deve ter até 50 caracteres.")]
        public string Id { get; private set; }

        [Required(ErrorMessage = "O pagamento é obrigatório.")]
        public Payment Payment { get; private set; }

        [Required(ErrorMessage = "O status do pagamento é obrigatório.")]
        public PaymentState Status { get; private set; }

        [StringLength(500, ErrorMessage = "Os detalhes devem ter até 500 caracteres.")]
        public string Details { get; private set; }

        [Required]
        public DateTime UpdatedAt { get; private set; }

        // Construtor privado para frameworks de serialização
        private PaymentStatus() { }

        public PaymentStatus(string id, PaymentState status, string details)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentException("O ID do pagamento não pode ser vazio ou nulo.", nameof(id));

            Id = id;
            Status = status;
            Details = details ?? throw new ArgumentNullException(nameof(details), "Detalhes não podem ser nulos.");
            UpdatedAt = DateTime.UtcNow;
        }

        public void UpdateStatus(PaymentState newStatus, string newDetails)
        {
            if (string.IsNullOrWhiteSpace(newDetails))
                throw new ArgumentException("Os detalhes do status não podem ser vazios ou nulos.", nameof(newDetails));

            Status = newStatus;
            Details = newDetails;
            UpdatedAt = DateTime.UtcNow;
        }
    }

    public enum PaymentState
    {
        Pending,    // Pagamento pendente
        Completed,  // Pagamento concluído
        Failed,     // Pagamento falhou
        Refunded    // Pagamento reembolsado
    }
}