using communication.Communication;
using communication.Communication.Nodes;
using communication.Models;
using Opc.UaFx.Client;
using System.Diagnostics;

namespace communication.Interfaces
{
    public interface IMachine
    {
        public Task<PowerState> Connect(PowerState powerState);
        public int GetProgress();
        public bool isConnected();
        public void Stop();
        public Command? GetCurrentCommand();
    }
}
