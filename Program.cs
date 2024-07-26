using System.Runtime.Serialization;
using ChecklistVeiculos.Models;
using ChecklistVeiculos.Persistence.Repositories;
using ChecklistVeiculos.Services;
using Microsoft.AspNetCore.Hosting.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

builder.Services.AddControllers();

builder.Services.AddLogging((config) =>
{
    config.ClearProviders(); // Limpa os providers de log padrão
    config.AddConsole(); // Adiciona o provider de log para console
    config.AddConfiguration(builder.Configuration.GetSection("Logging")); // Adiciona a configuração de log do appsettings.json
    config.AddSimpleConsole(); // Adiciona o provider de log para console com formato JSON, seguindo a    configuração do appsettings.json

});

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
        sqlServerOptionsAction: sqlOptions =>
        {
            sqlOptions.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName);
            sqlOptions.EnableRetryOnFailure(
                1,
                TimeSpan.FromMilliseconds(30),
                null
            );
            sqlOptions.UseCompatibilityLevel(150);
        });
});

builder.Services.AddScoped<ChecklistRepository>();
builder.Services.AddScoped<IGenericRepository<ChecklistVeiculoItemModel>, Repository<ChecklistVeiculoItemModel, ApplicationDbContext>>();
builder.Services.AddScoped<IGenericRepository<ChecklistVeiculoObservacaoModel>, Repository<ChecklistVeiculoObservacaoModel, ApplicationDbContext>>();
builder.Services.AddScoped<ChecklistVeiculosService>();

var app = builder.Build();
app.MapControllers(); // Adiciona o middleware de controllers
app.UseCors(); // Adiciona o middleware de CORS
app.UseRouting(); // Adiciona o middleware de roteamento
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options => // Swagger UI - Exibir a documentação gerada pelo Swagger na rota raiz
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });
}
else
{
    app.UseHttpsRedirection();
}




app.Run();

