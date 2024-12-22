using BarberShopScheduler.Api.Validations;
using System.ComponentModel.DataAnnotations;

namespace BarberShopScheduler.Api.Models
{
    public class Appointment
    {
        [Key]
        public Guid Id { get; set; } // Identificador único

        [Required(ErrorMessage = "A data do agendamento é obrigatória.")]
        public DateTime Date { get; set; } // Data do agendamento

        [Required(ErrorMessage = "O horário de início é obrigatório.")]
        public TimeSpan StartTime { get; set; } // Horário de início (HH:mm)

        [Required(ErrorMessage = "O horário de término é obrigatório.")]
        [CompareTimeSpan("StartTime", ErrorMessage = "O horário de término deve ser maior que o horário de início.")]
        public TimeSpan EndTime { get; set; } // Horário de término (HH:mm)

        [Required(ErrorMessage = "O nome do cliente é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome do cliente deve ter até 100 caracteres.")]
        public string CustomerName { get; set; } // Nome do cliente

        [Required(ErrorMessage = "O telefone do cliente é obrigatório.")]
        [RegularExpression(@"\(?\d{2}\)?\s?\d{4,5}-?\d{4}", ErrorMessage = "O telefone deve estar no formato válido (ex: (11) 91234-5678).")]
        public string CustomerPhone { get; set; } // Contato do cliente

        [Required(ErrorMessage = "A descrição do serviço é obrigatória.")]
        [StringLength(200, ErrorMessage = "A descrição do serviço deve ter até 200 caracteres.")]
        public string ServiceDescription { get; set; } // Serviço agendado

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Required]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Relacionamento com BarberShop
        [Required(ErrorMessage = "O identificador da barbearia é obrigatório.")]
        public Guid BarberShopId { get; set; } // Identificador da barbearia

        public Appointment() { } // Construtor padrão para EF Core

        public Appointment(
            Guid barberShopId,
            DateTime date,
            TimeSpan startTime,
            TimeSpan endTime,
            string customerName,
            string customerPhone,
            string serviceDescription)
        {
            Id = Guid.NewGuid();
            BarberShopId = barberShopId;
            Date = date;
            StartTime = startTime;
            EndTime = endTime;
            CustomerName = customerName;
            CustomerPhone = customerPhone;
            ServiceDescription = serviceDescription;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }

        public void UpdateDetails(string customerName, string customerPhone, string serviceDescription)
        {
            CustomerName = customerName;
            CustomerPhone = customerPhone;
            ServiceDescription = serviceDescription;
            UpdatedAt = DateTime.UtcNow;
        }
    }    
}
