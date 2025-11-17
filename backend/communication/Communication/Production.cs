using communication.Communication.Nodes;
using communication.Interfaces;
using communication.Models;
using Opc.Ua;
using Opc.Ua.Client;
using Opc.UaFx.Client;
using System;
using System.Diagnostics;
using Org.BouncyCastle.Tls;

namespace communication.Communication
{
    public class Production : IProduction
    {
        private static Production? instance;
        private static int timeoutMs = 5000; // The timeout for any opc ua action
        private static int publishInterval = 1000; // The default interval between publishes
        private List<IMachine> machines = new();
        private CommandQueue cmdQueue = new CommandQueue();

        private PowerState state; 
        
        private Production()
        {
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

                 machines.ForEach(machine => machine.Stop());

                 state = powerState;
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
        public void NewCommand(Command command) => cmdQueue.Enqueue(command);
        public bool DeleteCommand(Guid id) => cmdQueue.Delete(id);
        public Command?[] GetCurrentCommands() => machines.Select(m => m.GetCurrentCommand()).ToArray();
        public int[] GetProgress() => machines.Select(m => m.GetProgress()).ToArray();

        public static int GetTimeout() => timeoutMs;
        public static int GetPublishInterval() => publishInterval;
        public PowerState GetState() => state;
        
        public void MakeNewMachine(string connectionString)
        {
            machines.Add(new Machine(connectionString, cmdQueue));
        }
        public void MakeNewMachine(IMachine machine)
        {
            machines.Add(machine);
        }
        public MachineStatus GetStatus(string connectionString)
        {
            var machine = machines.FirstOrDefault(m => connectionString == m.GetConnectionString());
            if (machine == null)
            {
                throw new Exception($"No machine found with connection string '{connectionString}'");
            }
            return machine.GetStatus();
        }
    }
}
