using system;
using System.Threading;
using System.Threading.Tasks;
using communication.Communication.Nodes;
using communication.Models;

namespace DefaultNamespace;

public class SSEMachineStatusService
{
    //This class is a service, that connect "machine client" to "the beer machine" and updates "MachineStatus" Class
    //so the SSE (SSEMachineController) can send the lates information about the machine to frontend
    
    private readonly Machine _machine;
    private readonly MachineStatus _status;
    
    //Constructor to initiate machine client
    public SSEMachineStatusService(Machine machine)
    {
        _machine = machine;
        _status = MachineStatus.GetInstance();
    }
    
    //Method to read from client the latest information about the beer machine
    private void UpdateStatus(object? state)
    {
        try
        {
            if (!_machine.Connected)
                return;

            var client = _machine.client;

            // Get info about the the beer machine via NodeLib class 
            _status.MaintanenceCount = NodeLib.MaintenanceCount.Get(client);
            _status.BarleyCount = NodeLib.Barley.Get(client);
            _status.HopsCount = NodeLib.Hops.Get(client);
            _status.MaltCount = NodeLib.Malt.Get(client);
            _status.WheatCount = NodeLib.Wheat.Get(client);
            _status.YeastCount = NodeLib.Yeast.Get(client);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }
}