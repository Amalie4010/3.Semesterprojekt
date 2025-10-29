using communication.Communication.Nodes;
using communication.Models;
using Opc.Ua;
using Opc.Ua.Client;
using Opc.UaFx.Client;
using System;
using System.Diagnostics;
namespace communication.Communication
{
    public class Production
    {
        private static Production? instance;
        public static int timeoutMs = 5000; // The timeout for any opc ua action
        public static int publishInterval = 100; // The default interval between publishes
        private Dictionary<BeerTypes, Machine> machines = new();
        public PowerState State { get; private set; }
        
        private Production()
        {
            machines.Add(BeerTypes.Pilsner, new Machine(BeerTypes.Pilsner, "opc.tcp://localhost:4840"));
        }
        public static Production GetInstance()
        {
            if (instance == null)
            {
                instance = new Production();
            }
            return instance;
        }
        

        // (TimeOut exceptions are handled in machine.Connect.)
        public async Task<PowerState> Power(PowerState powerState)
        {
            try
            {
                 List<Task<PowerState>> connectTasks = new();
             
                 // Start async connect tasks
                 foreach(var kvp in machines)
                 {
                     var machine = kvp.Value;
                     connectTasks.Add(machine.Connect(powerState));
                 }
            
                 // Wait for all tasks to finish
            
                 var results = await Task.WhenAll(connectTasks);
            
                 return powerState; // Return desired value
            } 
            catch (Exception e)
            {
                if (e is TimeoutException)
                {
                    // Reverse action if any doesnt power on to ensure continuity across all machines
                    PowerState psOpposite = powerState == PowerState.On ? PowerState.Off : PowerState.On; // Reverse the powerstate
                    foreach (var kvp in machines)
                    {
                        var machine = kvp.Value;
                        await machine.Connect(psOpposite);
                    }
                }
                throw; // Rethrow to handle in controller
            }
        }
    
        public void NewCommand(Command command)
        {
            machines[command.Type].EnqueueCommand(command);
        }
    }
}
