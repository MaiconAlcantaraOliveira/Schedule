using BarberShopScheduler.Api;
using BarberShopScheduler.Api.Interfaces;
using BarberShopScheduler.Api.Services;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

var builder = WebApplication.CreateBuilder(args);

// Configuração da string de conexão para o banco de dados MySQL
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 40))));

// Configuração de Serviços
builder.Services.AddScoped<IBarberShopService, BarberShopService>();
builder.Services.AddScoped<IAppointmentService, AppointmentService>();
builder.Services.AddScoped<IPaymentService, PaymentService>();

// Configuração de CORS (ajustado para produção)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowProduction", builder =>
         builder.AllowAnyOrigin() // Permite qualquer origem
              .AllowAnyMethod() // Permite qualquer método HTTP (GET, POST, PUT, DELETE, etc.)
              .AllowAnyHeader()); // Pe
});

// Adiciona controllers
builder.Services.AddControllers();

// Configuração do Swagger (geralmente desativado em produção)
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

// Configuração do Kestrel (fora do app pipeline)
builder.WebHost.ConfigureKestrel(options =>
{
    // Configura Kestrel para escutar no IP público e nas portas desejadas
    options.ListenLocalhost(5211);  // Escutando na porta HTTP
    options.ListenLocalhost(7035, listenOptions =>
    //options.Listen(System.Net.IPAddress.Parse("127.0.0.1"), 5211);  // Escutando na porta HTTP
    //options.Listen(System.Net.IPAddress.Parse("127.0.0.1"), 7035, listenOptions =>
    //options.Listen(System.Net.IPAddress.Parse(host), 5211);  // Escutando na porta HTTP
    //options.Listen(System.Net.IPAddress.Parse(host), 7035, listenOptions =>

    {
        // Configuração de SSL
        var certificatePath = @"Certificate\BarberShopSchedulerCert.cer";  // Substitua pelo caminho do seu certificado
        //var certificatePassword = "Y6t5r4e3w2q1@1";  // Substitua pela senha do seu certificado
        listenOptions.UseHttps(certificatePath);  // Habilita o HTTPS
    });

});

var app = builder.Build();

// Configuração do pipeline de requisições
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BarberShopScheduler.Api v1"));
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();  // Habilita o HSTS em produção para maior segurança
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseCors("AllowProduction"); // CORS para produção

app.UseRouting();
app.UseAuthorization();

app.MapControllers();

// Inicializa o aplicativo
app.Run();
