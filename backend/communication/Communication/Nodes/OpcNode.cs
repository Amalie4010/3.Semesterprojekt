using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Opc.Ua.Client;
using Opc.UaFx.Client;
using System.Reflection.Metadata.Ecma335;

namespace communication.Communication.Nodes
{
    public class OpcNode<T> : OpcNodeBase
    {
        private readonly string nodeid;
        private OpcSubscription? subscription;
        public OpcNode(string nodeid)
        {
            this.nodeid = nodeid;
        }
        // Destructor. Runs when object is garbage collected
        ~OpcNode() 
        {
            if (subscription != null)
                subscription.Unsubscribe();
        }

        public OpcSubscription? Subscription
        {
            get { return subscription; }
        }

        public T Value
        {
            get { return GetValue(); }
            set { SetValue(value); }
        }
        private T GetValue()
        {
            T value = (T)client.ReadNode(nodeid).Value;
            return value;
        }
        private void SetValue(T value)
        {
            var r = client.WriteNode(nodeid, value);
            if (r.IsBad)
            {
                throw new Exception($"Could net set node with nodeid: '{nodeid}'.\n Reason: {r.Description}");
            }
        }

        public void SetSubscription(OpcDataChangeReceivedEventHandler func, int intervalMs)
        {
            subscription = client.SubscribeDataChange(nodeid, func);
            subscription.PublishingInterval = intervalMs;
            subscription.ApplyChanges();
        }
    }
}
