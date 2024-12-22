using BarberShopScheduler.Api;
using BarberShopScheduler.Api.Interfaces;
using BarberShopScheduler.Api.Services;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

var builder = WebApplication.CreateBuilder(args);

// Configura��o da string de conex�o para o banco de dados MySQL
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 40))));

// Configura��o de Servi�os
builder.Services.AddScoped<IBarberShopService, BarberShopService>();
builder.Services.AddScoped<IAppointmentService, AppointmentService>();
builder.Services.AddScoped<IPaymentService, PaymentService>();

// Configura��o de CORS (ajustado para produ��o)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowProduction", builder =>
         builder.AllowAnyOrigin() // Permite qualquer origem
              .AllowAnyMethod() // Permite qualquer m�todo HTTP (GET, POST, PUT, DELETE, etc.)
              .AllowAnyHeader()); // Pe
});

// Adiciona controllers
builder.Services.AddControllers();

// Configura��o do Swagger (geralmente desativado em produ��o)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var host = "127.0.0.1";

if (builder.Environment.IsDevelopment())
{
    host = "127.0.0.1";
}
else
{
    host = "192.168.2.242";
}

// Configura��o do Kestrel (fora do app pipeline)
builder.WebHost.ConfigureKestrel(options =>
{
    // Configura Kestrel para escutar no IP p�blico e nas portas desejadas
    options.ListenLocalhost(5211);  // Escutando na porta HTTP
    options.ListenLocalhost(7035, listenOptions =>
    //options.Listen(System.Net.IPAddress.Parse("127.0.0.1"), 5211);  // Escutando na porta HTTP
    //options.Listen(System.Net.IPAddress.Parse("127.0.0.1"), 7035, listenOptions =>
    //options.Listen(System.Net.IPAddress.Parse(host), 5211);  // Escutando na porta HTTP
    //options.Listen(System.Net.IPAddress.Parse(host), 7035, listenOptions =>

    {
        // Configura��o de SSL
        var certificatePath = @"Certificate\BarberShopSchedulerCert.cer";  // Substitua pelo caminho do seu certificado
        //var certificatePassword = "Y6t5r4e3w2q1@1";  // Substitua pela senha do seu certificado
        listenOptions.UseHttps(certificatePath);  // Habilita o HTTPS
    });

});

var app = builder.Build();

// Configura��o do pipeline de requisi��es
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BarberShopScheduler.Api v1"));
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();  // Habilita o HSTS em produ��o para maior seguran�a
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseCors("AllowProduction"); // CORS para produ��o

app.UseRouting();
app.UseAuthorization();

app.MapControllers();

// Inicializa o aplicativo
app.Run();
