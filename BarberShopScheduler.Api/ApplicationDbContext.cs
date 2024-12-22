using BarberShopScheduler.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace BarberShopScheduler.Api
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<BarberShop> BarberShops { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<PaymentStatus> PaymentStatus { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    // Configuração para BarberShop
        //    modelBuilder.Entity<BarberShop>(entity =>
        //    {
        //        entity.HasKey(b => b.Id);
        //        entity.Property(b => b.Name).IsRequired().HasMaxLength(100);
        //        entity.Property(b => b.Address).HasMaxLength(200);
        //        entity.Property(b => b.PhoneNumber).HasMaxLength(15);
        //        entity.Property(b => b.Email).HasMaxLength(100);
        //        entity.Property(b => b.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
        //        entity.Property(b => b.UpdatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
        //    });

        //    // Configuração para Appointment
        //    modelBuilder.Entity<Appointment>(entity =>
        //    {
        //        entity.HasKey(a => a.Id);
        //        entity.Property(a => a.Date).IsRequired();
        //        entity.Property(a => a.StartTime.ToString()).HasMaxLength(5);  // Definir tamanho para HH:mm
        //        entity.Property(a => a.EndTime.ToString()).HasMaxLength(5);    // Definir tamanho para HH:mm
        //        entity.Property(a => a.CustomerName).HasMaxLength(100);
        //        entity.Property(a => a.ServiceDescription).HasMaxLength(200);
        //        entity.HasOne(a => a.BarberShop)
        //              .WithMany(b => b.Appointments)
        //              .HasForeignKey(a => a.BarberShopId)
        //              .OnDelete(DeleteBehavior.Cascade);
        //    });

        //    // Configuração para Payment
        //    modelBuilder.Entity<Payment>(entity =>
        //    {
        //        entity.HasKey(p => p.PaymentId);  // Usar PaymentId como chave primária
        //        entity.Property(p => p.Amount).HasColumnType("decimal(18,2)");
        //        entity.Property(p => p.Date).IsRequired();
        //        entity.Property(p => p.PaymentMethod).HasMaxLength(50);  // Usar PaymentMethod em vez de Method
        //        entity.HasOne(p => p.BarberShop)
        //              .WithMany(b => b.Payments)
        //              .HasForeignKey(p => p.BarberShopId)
        //              .OnDelete(DeleteBehavior.Cascade);
        //    });

        //    // Configuração para PaymentStatus
        //    modelBuilder.Entity<PaymentStatus>(entity =>
        //    {
        //        entity.HasKey(ps => ps.Id);  // Usar PaymentId como chave primária
        //        entity.Property(ps => ps.Status).HasMaxLength(20);
        //        entity.Property(ps => ps.Details).HasMaxLength(500);
        //        entity.HasOne(ps => ps.Payment)
        //              .WithOne()
        //              .HasForeignKey<PaymentStatus>(ps => ps.Id)
        //              .OnDelete(DeleteBehavior.Cascade);
        //    });
        //}
    }
}
