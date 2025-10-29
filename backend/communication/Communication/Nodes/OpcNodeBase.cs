using Opc.UaFx.Client;

namespace communication.Communication.Nodes
{
    public abstract class OpcNodeBase
    {
        protected static OpcClient client = Production.GetClient();
    }
}
