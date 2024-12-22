using Microsoft.AspNetCore.Http.HttpResults;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BarberShopScheduler.Api.Models
{
    public class BarberShop
    {
        [Key]
        public Guid Id { get; private set; }

        [Required(ErrorMessage = "O nome da barbearia é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome da barbearia deve ter até 100 caracteres.")]
        public string Name { get; private set; }

        [Required(ErrorMessage = "O nome do dono da barbearia é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome do dono deve ter até 100 caracteres.")]
        public string OwnerName { get; private set; }

        [StringLength(200, ErrorMessage = "O endereço deve ter até 200 caracteres.")]
        public string Address { get; private set; }

        [Required(ErrorMessage = "O telefone é obrigatório.")]
        [RegularExpression(@"\(?\d{2}\)?\s?\d{4,5}-?\d{4}", ErrorMessage = "O telefone deve estar no formato válido (ex: (11) 91234-5678).")]
        public string PhoneNumber { get; private set; }

        [EmailAddress(ErrorMessage = "O e-mail deve ser válido.")]
        [StringLength(100, ErrorMessage = "O e-mail deve ter até 100 caracteres.")]
        public string Email { get; private set; }

        [RegularExpression(@"^#(?:[0-9a-fA-F]{3}){1,2}$", ErrorMessage = "A cor deve ser um código hexadecimal válido (ex: #FFFFFF).")]
        public string ThemeColor { get; private set; } = "#FFFFFF";

        [Required]
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

        [Required]
        public DateTime UpdatedAt { get; private set; } = DateTime.UtcNow;

        public List<Payment> Payments { get; private set; } = new List<Payment>();

        private BarberShop() { } // Construtor privado para EF Core

        public BarberShop(
            string name,
            string ownerName,
            string address,
            string phoneNumber,
            string email,
            string? themeColor)
        {
            Name = name;
            OwnerName = ownerName;
            Address = address;
            PhoneNumber = phoneNumber;
            Email = email;
            ThemeColor = themeColor ?? ThemeColor;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }

        public void UpdateDetails(string name, string address, string phoneNumber, string themeColor)
        {
            Name = name;
            Address = address;
            PhoneNumber = phoneNumber;
            ThemeColor = themeColor ?? ThemeColor;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
