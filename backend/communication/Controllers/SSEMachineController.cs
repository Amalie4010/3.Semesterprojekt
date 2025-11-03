using System.Runtime.CompilerServices;
using communication.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Microsoft.AspNetCore.Http;

namespace DefaultNamespace
{
    [ApiController]
    [Route("api/communication/SSEMachine")]
    public class SSEMachineController : ControllerBase
    {
        /**********
         THIS CLASS IS THE ENDPOINT FOR SSE
         
         READ THIS IF YOU WANT TO FULLY UNDERSTAND HOW SSE WORKS IN C#
         https://dev.to/mayank_agarwal/implementing-real-time-updates-with-server-sent-events-sse-in-c-net-a-complete-guide-248l
        ***********/
       
        
        // Constructor
        public SSEMachineController()
        {
        }

        // GET method for the endpoint with EmptyResult. EmptyResult returns no specific data at the end of the session
        [HttpGet]
        
        /*
         * Cancellation token makes it possible so when the user closes the browser
         * that request should stop, otherwise the server keeps looping forever
         */
        public async Task<EmptyResult> GetMachineStatus(CancellationToken cancellationToken) 
        {
            // Return an SSE stream as an IAsyncEnumerable
            //IAsyncEnumaerable makes it possible to instead of returning all data at once, it returns items one by one like SSE does
            async IAsyncEnumerable<string> SseMachineStatus([EnumeratorCancellation] CancellationToken ct)
            {
                //Instantiate singleton machinestatus
                var machineStatus = MachineStatus.GetInstance();

                // Loop until the client disconnects
                while (!ct.IsCancellationRequested)
                {
                    // Convert machineStatus status into JSON
                    var json = JsonSerializer.Serialize(machineStatus);

                    //Yield the JSON as a SSE message. yield doesnt return everything at once it makes it possible to return one thing at the time like no polling
                    //Yield pause and resume later each time it produces a new data
                    //If it was nor return without "yield" then it returns once and the coinnection is lost
                    yield return $"data: {json}\n\n";

                    // Wait one second before sending the next update about the machine (machineStatus objekt)
                    await Task.Delay(2000, ct);
                }
            }

            // Return the stream as an SSE response . Need this for the browser to know its SSE
            Response.Headers.Add("Content-Type", "text/event-stream");
            
            //CAlls the SseMachineStatus Method
            await foreach (var item in SseMachineStatus(cancellationToken))
            {
                //Item is the chunks of data that is sent everytime time in sse
                await Response.WriteAsync(item);
                await Response.Body.FlushAsync();
                
                /*
                 When you send data with Response.WriteAsync(item),
                 c# stores it in an buffer before sending it
                 and for SSE, we need each message to arrive immediately so the method 
                 FlushAsync sends the data immediatly instead of storing it in the buffer
                 */
            }
            
            //Returns nothing if the connection is lost
            return new EmptyResult();
        }
    }
}