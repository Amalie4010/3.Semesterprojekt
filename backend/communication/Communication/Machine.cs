using communication.Communication.Nodes;
using communication.Models;
using Opc.Ua.Export;
using Opc.UaFx.Client;
using System.Diagnostics;
using System.Threading;

namespace communication.Communication
{
    public class Machine
    {
        public readonly OpcClient client; // The acutal Opc Ua client
        private readonly BeerTypes beerType;
        private readonly SemaphoreSlim connectSemaphore = new SemaphoreSlim(1, 1); // semaphore or Power method
        private int currentState = 0; // current PackML state
        private Queue<Command> cmdQueue = new();

        public bool Connected { get; private set; }

        public Machine(BeerTypes beerType, string opcUrl)
        {
            this.beerType = beerType;
            client = new OpcClient(opcUrl);

        }
        public async Task<PowerState> Connect(PowerState powerState)
        {
            await connectSemaphore.WaitAsync(); // Wait for space in code below
            try
            {
                // Create new tcs
                var tcs = new TaskCompletionSource<PowerState>();


                // Create event handler
                void Handler(object? sender, EventArgs? e)
                {
                    tcs.SetResult(powerState);
                    Connected = powerState == PowerState.On; // Whenn succesfull, set the Connected prop
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
                    Task.Delay(Production.timeoutMs)
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
            finally
            {
                connectSemaphore.Release();// Release semaphore
            }
        }
        private void SetupSubscribtions()
        {
            // No need to save the sub, since it will be removed when connection is lost anyway
            NodeLib.StateCurrent.AddSubscription(client, HandleStateCurrentChange, Production.publishInterval);
        }
        private void HandleStateCurrentChange(object sender, OpcDataChangeReceivedEventArgs e)
        {
            int state = (int)e.Item.Value.Value;
            currentState = state;
            switch (state)
            {
                case MachineState.Idle:
                    {
                        Debug.WriteLine($"State (Machine: {beerType}) changed to Idle");
                        HandleIdle();
                        break;
                    }
                case MachineState.Held:
                    {
                        Debug.WriteLine($"State (Machine: {beerType}) changed to Held");
                        break;
                    }
                case MachineState.Completed:
                    {
                        Debug.WriteLine($"State (Machine: {beerType}) changed to Completed");
                        NodeLib.CtrlCmd.Set(client, CtrlCommand.Reset);
                        NodeLib.CmdChangeRequest.Set(client, true);
                        break;
                    }
                case MachineState.Execute:
                    {
                        Debug.WriteLine($"State (Machine: {beerType}) changed to Execute");
                        break;
                    }
                case MachineState.Stopped:
                    {
                        Debug.WriteLine($"State (Machine: {beerType}) changed to Stopped");
                        NodeLib.CtrlCmd.Set(client, CtrlCommand.Reset);
                        NodeLib.CmdChangeRequest.Set(client, true);
                        break;
                    }
                case MachineState.Aborted:
                    {
                        Debug.WriteLine($"State (Machine: {beerType}) changed to Aborted");
                        break;
                    }
                default:
                    {
                        Debug.WriteLine($"State (Machine: {beerType}) changed to other");
                        break;
                    }
            }
        }

        public void EnqueueCommand(Command command)
        {
            cmdQueue.Enqueue(command);
        }
        private void HandleIdle()
        {
            if (cmdQueue.Count == 0)
            {
                return; //TODO: Instead of return, something something wait for a wake up
            }

            var command = cmdQueue.Dequeue();

            // Load command variables into machine
            NodeLib.ProductId.Set(client, (float)beerType);
            NodeLib.ProductsAmount.Set(client, command.Amount);
            NodeLib.MachSpeed.Set(client, command.Speed);

            // Start production
            NodeLib.CtrlCmd.Set(client, CtrlCommand.Start);
            NodeLib.CmdChangeRequest.Set(client, true);
        }
    }
}
