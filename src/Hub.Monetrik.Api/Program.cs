using FluentValidation;
using Hub.Monetrik.Domain.Behaviors;
using Hub.Monetrik.Domain.Commands.Despesas.Atualizar;
using Hub.Monetrik.Domain.Commands.Despesas.Cadastrar;
using Hub.Monetrik.Domain.Interfaces.Despesas;
using Hub.Monetrik.Domain.Interfaces.Repository;
using Hub.Monetrik.Domain.Models.Entities.Despesa;
using Hub.Monetrik.Domain.Notifications;
using Hub.Monetrik.Domain.Services.Despesas.Buscar;
using Hub.Monetrik.Mediator.Interfaces.Mediator;
using Hub.Monetrik.Mediator.Services;
using Hub.MoneTrik.Infrastructure.Context;
using Hub.MoneTrik.Infrastructure.Repositories.Despesas;
using Microsoft.EntityFrameworkCore;
using static Hub.Monetrik.Mediator.Interfaces.INotificationHandler;
using static Hub.Monetrik.Mediator.Interfaces.IPipelineBehavior;
using static Hub.Monetrik.Mediator.Interfaces.Mediator.IRequestHandler;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Swagger simples
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Despesas Services
builder.Services.AddScoped<IDespesas, BuscarDespesasService>();

// Mediator e Handlers
builder.Services.AddScoped<IMediator, SimpleMediatorService>();
builder.Services.AddScoped<IRequestHandler<CadastrarDespesasCommand, Despesa>, CadastrarDespesasCommandHandler>();
builder.Services.AddScoped<IRequestHandler<AtualizarSituacaoDespesaCommand, Despesa>, AtualizarSituacaoDespesaCommandHandler>();

// Notifications
builder.Services.AddScoped<NotificationHandler>();
builder.Services.AddScoped<INotificationHandler<Notification>>(sp => sp.GetRequiredService<NotificationHandler>());

// Pipeline Behaviors
builder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

// Validators
builder.Services.AddScoped<IValidator<CadastrarDespesasCommand>, CadastrarDespesasValidator>();
builder.Services.AddScoped<IValidator<AtualizarSituacaoDespesaCommand>, AtualizarSituacaoDespesaValidator>();

// Context - Repository
builder.Services.AddScoped<IDespesasRepository, DespesasRepository>();

// Configuração do DbContext
var connectionString = builder.Configuration["ConnectionStrings:DefaultConnection"] 
    ?? "Server=localhost;Database=hubmonetrik;User=dev;Password=dev123;";

builder.Services.AddDbContext<HubMonetrikContext>(options =>
    options.UseMySql(
        connectionString,
        ServerVersion.AutoDetect(connectionString),
        mysqlOptions => 
        {
            mysqlOptions.EnableRetryOnFailure(
                maxRetryCount: 5,
                maxRetryDelay: TimeSpan.FromSeconds(30),
                errorNumbersToAdd: null);
        }
    )
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
