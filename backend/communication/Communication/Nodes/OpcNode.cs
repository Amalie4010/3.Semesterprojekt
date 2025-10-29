using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Opc.Ua.Client;
using Opc.UaFx.Client;
using System.Reflection.Metadata.Ecma335;

namespace communication.Communication.Nodes
{
    // All needed instances of this class are (probably) instantiated in NodeLib.cs
    public class OpcNode<T>
    {
        private readonly string nodeid;
        public OpcNode(string nodeid)
        {
            this.nodeid = nodeid;
        }
        // Destructor. Runs when object is garbage collected
        ~OpcNode() 
        {
            if (Subscription != null)
                Subscription.Unsubscribe();
        }

        public OpcSubscription? Subscription { get; private set; }

        public T GetValue(OpcClient client)
        {
            T value = (T)client.ReadNode(nodeid).Value;
            return value;
        }
        public void SetValue(OpcClient client, T value)
        {
            var r = client.WriteNode(nodeid, value);
            if (r.IsBad)
            {
                throw new Exception($"Could net set node with nodeid: '{nodeid}'.\n Reason: {r.Description}");
            }
        }

        public OpcSubscription AddSubscription(OpcClient client, OpcDataChangeReceivedEventHandler func, int intervalMs)
        {
            var sub = client.SubscribeDataChange(nodeid, func);
            sub.PublishingInterval = intervalMs;
            sub.ApplyChanges();
            return sub;
        }
    }
}
