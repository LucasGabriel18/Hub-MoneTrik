using Hub.Monetrik.Domain.Notifications;
using Hub.Monetrik.Mediator.Interfaces;
using Hub.Monetrik.Mediator.Interfaces.Mediator;
using Hub.Monetrik.Mediator.Services;
using static Hub.Monetrik.Mediator.Interfaces.INotificationHandler;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Swagger simples
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Mediator DI
builder.Services.AddScoped<IMediator, SimpleMediatorService>();
builder.Services.AddScoped<INotificationHandler<Notification>, NotificationHandler>();


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
