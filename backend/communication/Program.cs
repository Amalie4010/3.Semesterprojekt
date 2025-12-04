using communication.Communication;
using communication.Controllers;
using communication.Interfaces;

using communication.Models;
using DefaultNamespace;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLaravel", policy =>
    {
        policy.WithOrigins("http://127.0.0.1:8000")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

// Add services to the container.
builder.Services.AddControllers()
    .AddControllersAsServices();
// Register controller manually
builder.Services.AddTransient<MachineController>(sp =>
    new MachineController() // your own constructor
);
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

app.UseCors("AllowLaravel");


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

