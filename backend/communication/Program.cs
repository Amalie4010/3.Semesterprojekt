//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.

//builder.Services.AddControllers();
//// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();


//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();

//app.UseAuthorization();

//app.MapControllers();

//app.Run();
using communication.Communication;
using communication.Models;
using System.Diagnostics;

await Task.Delay(1000);
var m = Machine.GetInstance();
Debug.WriteLine($"System Status: {await m.Power(PowerState.On)}");
await Task.Delay(1000);
m.testWrite(CtrlCommand.Reset);
await Task.Delay(1000);


Debug.WriteLine($"System Status: {await m.Power(PowerState.Off)}");
