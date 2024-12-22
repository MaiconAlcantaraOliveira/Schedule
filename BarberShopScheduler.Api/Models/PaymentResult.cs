using System;
using System.ComponentModel.DataAnnotations;

namespace BarberShopScheduler.Api.Models
{
    public class PaymentResult
    {
        [Required]
        public bool Success { get; private set; }

        [StringLength(50, ErrorMessage = "O identificador da transação deve ter até 50 caracteres.")]
        public string? TransactionId { get; private set; }

        [Required(ErrorMessage = "A mensagem é obrigatória.")]
        [StringLength(200, ErrorMessage = "A mensagem deve ter até 200 caracteres.")]
        public string Message { get; private set; }

        [Required(ErrorMessage = "A data de processamento é obrigatória.")]
        public DateTime ProcessedAt { get; private set; }

        private PaymentResult() { } // Construtor privado para frameworks de serialização

        public PaymentResult(bool success, string? transactionId, string message, DateTime? processedAt = null)
        {
            Success = success;

            if (success && string.IsNullOrWhiteSpace(transactionId))
                throw new ArgumentNullException(nameof(transactionId), "Transação bem-sucedida deve ter um ID de transação.");

            TransactionId = success ? transactionId : null;
            Message = message ?? throw new ArgumentNullException(nameof(message), "Mensagem não pode ser nula.");
            ProcessedAt = processedAt ?? DateTime.UtcNow;
        }

        public static PaymentResult Successful(string transactionId, string message)
        {
            if (string.IsNullOrWhiteSpace(transactionId))
                throw new ArgumentException("O ID da transação é obrigatório para um resultado bem-sucedido.", nameof(transactionId));

            return new PaymentResult(true, transactionId, message);
        }

        public static PaymentResult Failed(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
                throw new ArgumentException("A mensagem de erro é obrigatória para um resultado falho.", nameof(message));

            return new PaymentResult(false, null, message);
        }

        public void UpdateMessage(string newMessage)
        {
            if (string.IsNullOrWhiteSpace(newMessage))
                throw new ArgumentException("A mensagem não pode ser vazia ou nula.", nameof(newMessage));

            Message = newMessage;
        }
    }
}
