using System;
using System.ComponentModel.DataAnnotations;

namespace BarberShopScheduler.Api.Models
{
    public class ServiceResult
    {
        [Required]
        public bool Success { get; private set; }

        [Required(ErrorMessage = "A mensagem é obrigatória.")]
        [StringLength(500, ErrorMessage = "A mensagem deve ter até 500 caracteres.")]
        public string Message { get; private set; }

        [Required]
        public DateTime Timestamp { get; private set; }

        // Construtor privado para frameworks de serialização
        private ServiceResult() { }

        private ServiceResult(bool success, string message)
        {
            if (string.IsNullOrWhiteSpace(message))
                throw new ArgumentException("A mensagem não pode ser vazia ou nula.", nameof(message));

            Success = success;
            Message = message;
            Timestamp = DateTime.UtcNow;
        }

        public static ServiceResult Successful(string message)
        {
            return new ServiceResult(true, message);
        }

        public static ServiceResult Failure(string message)
        {
            return new ServiceResult(false, message);
        }
    }
}