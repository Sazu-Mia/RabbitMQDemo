using MassTransit;
using Microsoft.EntityFrameworkCore;
using PayrollManagement.Consumer;
using PayrollManagement.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnectionString")));

builder.Services.AddMassTransit(x =>
{
    // Automatically register all consumers in this assembly
    x.AddConsumers(typeof(Program).Assembly);

    x.UsingRabbitMq((context, cfg) =>
    {
        // RabbitMQ host configuration
        cfg.Host("localhost", "/", h => { });

        // Automatically create queues for all registered consumers
        cfg.ConfigureEndpoints(context);
    });
});

//builder.Services.AddHostedService<RabbitMqListenerService>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

// MassTransit with RabbitMQ



builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
