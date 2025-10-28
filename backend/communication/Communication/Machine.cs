using communication.Models;
using Opc.UaFx.Client;
using System.Diagnostics;
namespace communication.Communication
{
    public class Machine
    {
        private static Machine? instance;
        private Queue<Command> cmdQueue = new Queue<Command>();
        private string opcUrl = "opc.tcp://localhost:4840";
        private int timeoutMs = 5000;
        private PowerState powerState = PowerState.Off;
        private OpcClient client;
        private List<OpcSubscription> subscriptions = new List<OpcSubscription>();
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
        private void SetupSubscribtions()
        {
            var stateCurrentSub = client.SubscribeDataChange(NodeLib.StateCurrent, HandleStateCurrentChange);
            stateCurrentSub.PublishingInterval = 100;
            subscriptions.Add(stateCurrentSub);
            stateCurrentSub.ApplyChanges();
        }
        private void DestroySubscribtions()
        {
            foreach (var sub in subscriptions)
            {
                sub.Unsubscribe();
            }
        }

        public async Task<PowerState> Power(PowerState powerState)
        {
            // Create new tcs
            var tcs = new TaskCompletionSource<PowerState>();
            

            // Create event handler
            void Handler(object? sender, EventArgs? e)
            {
                tcs.SetResult(powerState);
                this.powerState = powerState;
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
            Debug.WriteLine($"State changed: {e.Item.Value.ToString()}");
        }

        //public void testRead()
        //{
        //    var r = client.ReadNode(NodeLib.StateCurrent);
        //    Debug.WriteLine(r.ToString());
        //}
        //public void testWrite(float value)
        //{
        //    client.WriteNode(NodeLib.CtrlCmd, value);
        //    client.WriteNode(NodeLib.CmdChangeRequest, true);
        //}

    }
}
