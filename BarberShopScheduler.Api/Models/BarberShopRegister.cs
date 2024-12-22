using System;
using System.ComponentModel.DataAnnotations;

namespace BarberShopScheduler.Api.Models
{
    // Modelo para registro de uma barbearia
    public class BarberShopRegister
    {
        [Key]
        public Guid Id { get; private set; }

        [Required(ErrorMessage = "O nome da barbearia é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome da barbearia deve ter até 100 caracteres.")]
        public string Name { get; private set; }

        [Required(ErrorMessage = "O endereço é obrigatório.")]
        [StringLength(200, ErrorMessage = "O endereço deve ter até 200 caracteres.")]
        public string Address { get; private set; }

        [Required(ErrorMessage = "O número de telefone é obrigatório.")]
        [RegularExpression(@"\(?\d{2}\)?\s?\d{4,5}-?\d{4}", ErrorMessage = "O telefone deve estar no formato válido (ex: (11) 91234-5678).")]
        public string PhoneNumber { get; private set; }

        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "O e-mail deve ser válido.")]
        [StringLength(100, ErrorMessage = "O e-mail deve ter até 100 caracteres.")]
        public string Email { get; private set; }

        [Required(ErrorMessage = "O nome do proprietário é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome do proprietário deve ter até 100 caracteres.")]
        public string OwnerName { get; private set; }

        [Required]
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

        [Required]
        public DateTime ModifiedAt { get; private set; } = DateTime.UtcNow;

        private BarberShopRegister() { } // Construtor privado para EF Core

        public BarberShopRegister(string name, string address, string phoneNumber, string email, string ownerName)
        {
            Name = name;
            Address = address;
            PhoneNumber = phoneNumber;
            Email = email;
            OwnerName = ownerName;
            CreatedAt = DateTime.UtcNow;
            ModifiedAt = DateTime.UtcNow;
        }

        public void UpdateDetails(string name, string address, string phoneNumber, string email, string ownerName)
        {
            Name = name;
            Address = address;
            PhoneNumber = phoneNumber;
            Email = email;
            OwnerName = ownerName;
            ModifiedAt = DateTime.UtcNow;
        }
    }
}
