using communication.Communication;
using communication.Dtos;
using communication.Models;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StockPriceAPI.Model; 
using System.Text.Json;
using communication.Models;

namespace DefaultNamespace;
{
    
    // ********** IF YOU WANT TO UNDERSTAND THIS FULLY SEE THIS VIDEO: https://www.youtube.com/watch?v=x0725PDUho8 **********
    
    
    [ApiController]
    [Route("api/communication/SSEMachine")]
    public class SSEMachineController : ControllerBase
    {
        // Constructor
        public SSEMachineController()
        {
        }

        // GET method for the endpoint - with IResult type. IResult makes it possible to return an object thats represented as http response
        [HttpGet]
        public IResult GetMachineStatus(CancellationToken cancellationToken)
        {
            // Return an SSE stream as an IAsyncEnumerable
            //IAsyncEnumaerable makes it possible to instead of returning all data at once, it returns items one by one asynchronously like SSE does
            async IAsyncEnumerable<string> StreamMachineStatus([EnumeratorCancellation] CancellationToken ct)
            {
                //Returns singleton machinestatus
                var machineStatus = MachineStatus.GetInstance();

                // Loop until the client disconnects
                while (!ct.IsCancellationRequested)
                {
                    // Convert machineStatus status into JSON
                    var json = JsonSerializer.Serialize(machineStatus);

                    // Yield the JSON as a SSE message : Yield doesnt return everything at once it makes it possible to return one thing at the time like no polling
                    //Yield pause and resume later each time it produces a new data
                    //If it was nor return without "yield" then it returns once and the coinnection is lost
                    yield return $"data: {json}\n\n";

                    // Wait one second before sending the next update about the machine (machineStatus objekt)
                    await Task.Delay(1000, ct);
                }
            }

            // Return the SSE stream as the response
            return TypedResults.ServerSentEvents(StreamMachineStatus(cancellationToken));
        }
    }
}