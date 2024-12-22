using System;
using System.ComponentModel.DataAnnotations;

namespace BarberShopScheduler.Api.Models
{
    public class BarberShopUpdate
    {
        [Key]
        public Guid Id { get; private set; }

        [StringLength(100, ErrorMessage = "O nome da barbearia deve ter até 100 caracteres.")]
        public string? Name { get; private set; }

        [StringLength(200, ErrorMessage = "O endereço deve ter até 200 caracteres.")]
        public string? Address { get; private set; }

        [RegularExpression(@"\(?\d{2}\)?\s?\d{4,5}-?\d{4}", ErrorMessage = "O telefone deve estar no formato válido (ex: (11) 91234-5678).")]
        public string? PhoneNumber { get; private set; }

        [EmailAddress(ErrorMessage = "O e-mail deve ser válido.")]
        [StringLength(100, ErrorMessage = "O e-mail deve ter até 100 caracteres.")]
        public string? Email { get; private set; }

        [Required]
        public DateTime ModifiedAt { get; private set; } = DateTime.UtcNow;

        private BarberShopUpdate() { } // Construtor privado para EF Core ou frameworks de serialização

        public BarberShopUpdate(Guid id)
        {
            Id = id;
            ModifiedAt = DateTime.UtcNow;
        }

        public void UpdateDetails(string? name, string? address, string? phoneNumber, string? email)
        {
            Name = !string.IsNullOrWhiteSpace(name) ? name : Name;
            Address = !string.IsNullOrWhiteSpace(address) ? address : Address;
            PhoneNumber = !string.IsNullOrWhiteSpace(phoneNumber) ? phoneNumber : PhoneNumber;
            Email = !string.IsNullOrWhiteSpace(email) ? email : Email;
            ModifiedAt = DateTime.UtcNow;
        }
    }
}
