using FluentValidation;
using Hub.Monetrik.Domain.Behaviors;
using Hub.Monetrik.Domain.Commands.Despesas.Cadastrar;
using Hub.Monetrik.Domain.Models.Entities.Despesa;
using Hub.Monetrik.Domain.Notifications;
using Hub.Monetrik.Mediator.Interfaces.Mediator;
using Hub.Monetrik.Mediator.Services;
using static Hub.Monetrik.Mediator.Interfaces.INotificationHandler;
using static Hub.Monetrik.Mediator.Interfaces.IPipelineBehavior;
using static Hub.Monetrik.Mediator.Interfaces.Mediator.IRequestHandler;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Swagger simples
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Mediator e Handlers
builder.Services.AddScoped<IMediator, SimpleMediatorService>();
builder.Services.AddScoped<IRequestHandler<CadastrarDespesasCommand, Despesa>, CadastrarDespesasCommandHandler>();

// Notifications
builder.Services.AddScoped<INotificationHandler<Notification>, NotificationHandler>();
builder.Services.AddScoped<NotificationHandler>();

// Pipeline Behaviors
builder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

// Validators
builder.Services.AddScoped<IValidator<CadastrarDespesasCommand>, CadastrarDespesasValidator>();

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
