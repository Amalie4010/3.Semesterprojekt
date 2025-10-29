using communication.Communication.Nodes;
using communication.Models;
using Opc.Ua;
using Opc.Ua.Client;
using Opc.UaFx.Client;
using System;
using System.Diagnostics;
namespace communication.Communication
{
    public class Machine
    {
        private static Machine? instance;
        private Queue<Command> cmdQueue = new Queue<Command>();
        private string opcUrl = "opc.tcp://localhost:4840";
        private int timeoutMs = 5000;
        private int publishInterval = 100;
        private OpcClient client;
        
        public PowerState State { get; private set; }
        private Machine()
        {
            // Create client on URL
            client = new OpcClient(opcUrl);
            cmdQueue.Enqueue(new Command(BeerTypes.Pilsner, 100, 150));
        }
        public static Machine GetInstance()
        {
            if (instance == null)
            {
                instance = new Machine();
            }
            return instance;
        }
        public static OpcClient GetClient()
        {
            if (instance == null)
            {
                instance = GetInstance();
            }
            if (instance.client == null)
            {
                throw new Exception("Something went wrong whilst creating the opc client in machine");
            }
            return instance.client;
        }
        private void SetupSubscribtions()
        {
            NodeLib.StateCurrent.SetSubscription(HandleStateCurrentChange, publishInterval); 
        }

        public async Task<PowerState> Power(PowerState powerState)
        {
            // Create new tcs
            var tcs = new TaskCompletionSource<PowerState>();


            // Create event handler
            void Handler(object? sender, EventArgs? e)
            {
                tcs.SetResult(powerState);
                State = powerState;
            }

            // Determine weather to wait for connected or disconnected
            if (powerState == PowerState.On)
            {
                client.Connected += Handler;
                client.Connect();
            }
            else
            {
                client.Disconnected += Handler;
                client.Disconnect();
            }


            // Wait for connection or timeout
            var t = await Task.WhenAny(
                tcs.Task,
                Task.Delay(timeoutMs)
                );

            // Cleanup handler
            if (powerState == PowerState.On)
            {
                client.Connected -= Handler;
                SetupSubscribtions();
            }
            else
            {
                client.Disconnected -= Handler;
                //DestroySubscribtions();
            }

            // Handle timeout
            if (t != tcs.Task)
            {
                throw new TimeoutException("Connection timed out");
            }

            // Return result
            return await tcs.Task;
        }

        private void HandleStateCurrentChange(object sender, OpcDataChangeReceivedEventArgs e)
        {
            int state = (int)e.Item.Value.Value;
            switch(state)
            {
                case MachineState.Idle:
                    {
                        Debug.WriteLine($"State changed to Idle");
                        break;
                    }
                case MachineState.Held:
                    {
                        Debug.WriteLine($"State changed to Held");
                        break;
                    }
                case MachineState.Completed:
                    {
                        Debug.WriteLine($"State changed to Completed");
                        break;
                    }
                case MachineState.Execute:
                    {
                        Debug.WriteLine($"State changed to Execute");
                        break;
                    }
                case MachineState.Stopped:
                    {
                        Debug.WriteLine($"State changed to Stopped");
                        break;
                    }
                case MachineState.Aborted:
                    {
                        Debug.WriteLine($"State changed to Aborted");
                        break;
                    }
                default:
                    {
                        Debug.WriteLine("State changed to other");
                        break;
                    }
            }
        }
    }
}
