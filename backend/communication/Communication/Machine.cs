using communication.Communication.Nodes;
using communication.Interfaces;
using communication.Models;
using Opc.Ua.Export;
using Opc.UaFx.Client;
using Org.BouncyCastle.X509;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace communication.Communication
{
    public class Machine : IMachine
    {
        private readonly OpcClient client; // The acutal Opc Ua client
        private readonly SemaphoreSlim connectSemaphore = new SemaphoreSlim(1, 1); // semaphore for Power method
        private CancellationTokenSource stateChangedCts = new();
        private int currentState = 0; // current PackML state
        private CommandQueue cmdQueue;
        private Command? CurrentCommand;
        private bool Connected;

        public Machine(string opcUrl, CommandQueue cmdQueue)
        {
            this.cmdQueue = cmdQueue;
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
                    Task.Delay(Production.GetTimeout())
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
            NodeLib.StateCurrent.AddSubscription(client, HandleStateCurrentChange, Production.GetPublishInterval());
        }
        private void HandleStateCurrentChange(object sender, OpcDataChangeReceivedEventArgs e)
        {
            int state = (int)e.Item.Value.Value;
            currentState = state;
            // Keep old cts ref, for cancelling later, when state change is done
            var oldCts = stateChangedCts;
            stateChangedCts = new();
            oldCts.Cancel(); // Cancel any waiters
            
            // Actually handle the new state
            switch (state)
            {
            case MachineState.Idle:
                {
                    Debug.WriteLine($"State changed to Idle");
                    _ = HandleIdle();
                    break;
                }
            case MachineState.Held:
                {
                    Debug.WriteLine($"State  changed to Held");
                    break;
                }
            case MachineState.Completed:
                {
                    Debug.WriteLine($"State changed to Completed");
                    CurrentCommand = null;
                    NodeLib.CtrlCmd.Set(client, CtrlCommand.Reset);
                    NodeLib.CmdChangeRequest.Set(client, true);
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
                    NodeLib.CtrlCmd.Set(client, CtrlCommand.Reset);
                    NodeLib.CmdChangeRequest.Set(client, true);
                    break;
                }
            case MachineState.Aborted:
                {
                    Debug.WriteLine($"State changed to Aborted");
                    break;
                }
            default:
                {
                    Debug.WriteLine($"State changed to other");
                    break;
                }
            }
        }

        private async Task HandleIdle()
        {

            Task<Command> queueTask = cmdQueue.Dequeue();
            // Wait for next command to be available
            try
            {
                await Task.WhenAny(
                    Task.Delay(Timeout.Infinite, stateChangedCts.Token),
                    queueTask
                    );
            }
            catch (TaskCanceledException)
            {
                return;
            }
            Command command = queueTask.Result;
            

            // Load command variables into machine
            NodeLib.ProductId.Set(client, (float)command.Type);
            NodeLib.ProductsAmount.Set(client, command.Amount);
            NodeLib.MachSpeed.Set(client, command.Speed);

            // Start production
            NodeLib.CtrlCmd.Set(client, CtrlCommand.Start);
            NodeLib.CmdChangeRequest.Set(client, true);

            CurrentCommand = command;
        }
        public int GetProgress()
        {
            ushort raw = NodeLib.ProducedAmount.Get(client);
            return Convert.ToInt32(raw);
        }
        public void Stop()
        {
            NodeLib.CtrlCmd.Set(client, CtrlCommand.Stop);
            NodeLib.CmdChangeRequest.Set(client, true);
        }
        public bool isConnected() => Connected;
        public Command? GetCurrentCommand() => CurrentCommand;
    }
}
