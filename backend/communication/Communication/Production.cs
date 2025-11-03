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
        private List<Machine> machines = new();
        private CommandQueue cmdQueue = new CommandQueue();

        public PowerState State { get; private set; }
        
        private Production()
        {
            machines.Add(new Machine("opc.tcp://localhost:4840", cmdQueue));
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
                 foreach(var machine in machines)
                 {
                     connectTasks.Add(machine.Connect(powerState));
                 }
            
                 // Wait for all tasks to finish
            
                 var results = await Task.WhenAll(connectTasks);

                 State = powerState;
                 return powerState; // Return desired value
            } 
            catch (Exception e)
            {
                if (e is TimeoutException)
                {
                    // Reverse action if any doesnt power on to ensure continuity across all machines
                    PowerState psOpposite = powerState == PowerState.On ? PowerState.Off : PowerState.On; // Reverse the powerstate
                    foreach (var machine in machines)
                    {
                        await machine.Connect(psOpposite);
                    }
                }
                throw; // Rethrow to handle in controller
            }
        }
    
        public void NewCommand(Command command)
        {
            cmdQueue.Enqueue(command);
        }
        public bool DeleteCommand(Guid id)
        {
            return cmdQueue.Delete(id);
        }
        public Command?[] GetCurrentCommands()
        {
            return machines.Select(m => m.CurrentCommand).ToArray();
        }
        public int[] GetProgress()
        {
            return machines.Select(m => m.GetProgress()).ToArray();
        }

    }
}
