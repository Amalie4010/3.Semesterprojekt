using communication.Communication;
using communication.Models;
using DefaultNamespace;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();


/*
 * This section is for SSE START
 */

// Register a single machine client
var machine = new Machine("opc.tcp://localhost:4840");
await machine.Connect(PowerState.On); // connect once at startup

// Instantiate a statusService with the machine/client
var statusService = new SSEMachineStatusService(machine);

//Method to call UpdateStatus to update the status of the machine every second
var timer = new System.Threading.Timer(_ => statusService.UpdateStatus(), null, 0, 1000);

/*
 * This section is for SSE END
 */



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

