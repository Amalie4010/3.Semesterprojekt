using System.Threading;
using System.Threading.Tasks;
using communication.Communication;
using communication.Communication.Nodes;
using communication.Models;

namespace DefaultNamespace;

public class SSEMachineStatusService
{
    /***********
     IMAGINE THIS CLASS AS A MIDDLEMAN FOR CONNECTING OPC-UA CLIENT WITH MACHINESTATUS CLASS:
      This class is a service, that connect "machine client / The beer machine" to "machineStatus" and updates "MachineStatus" Class
      with the latest status information about the machine,
      then the SSE (SSEMachineController) can send the latest information about the machine to frontend using machineStatus
      ***********/

    private readonly Machine _machine;
    private readonly MachineStatus _status = new();

    //Constructor to initiate machine client
    public SSEMachineStatusService(Machine machine)
    {
        _machine = machine;
    }
    public MachineStatus GetStatus() => _status;

    //Method to read from client the latest information about the beer machine
    public void UpdateStatus()
    {
        try
        {
            var _client = _machine.GetClient();
            if (_client == null || !_machine.isConnected())
            {
                Console.WriteLine("Machine client not connected yet!");
                return;
            }

            //.client after _machine is the actual UPC UA Client that is connected to the beer machine.
            //Im accesing it via a readonly object that is stored in mahine class.

            /* Get info about the beer machine via ReadNode method that Opc UA Client has and store them in machineStatus
            them to MachineStatus model
            */

            // MACHINE STATUS
            _status.StateCurrent = Convert.ToInt32(_client.ReadNode("ns=6;s=::Program:Cube.Status.StateCurrent").Value);
            _status.CurrentMachSpeed = Convert.ToSingle(_client.ReadNode("ns=6;s=::Program:Cube.Status.MachSpeed").Value);
            _status.StopReasonID = Convert.ToInt32(_client.ReadNode("ns=6;s=::Program:Cube.Admin.StopReason.Id").Value);
            _status.StopReasonValue = Convert.ToInt32(_client.ReadNode("ns=6;s=::Program:Cube.Admin.StopReason.Value").Value);

            //PRODUCTION DATA
            _status.BatchId = Convert.ToSingle(_client.ReadNode("ns=6;s=::Program:batch_id").Value);
            _status.CurrentProduct = Convert.ToSingle(_client.ReadNode("ns=6;s=::Program:Cube.Admin.Parameter[0].Value").Value);
            _status.ProducedAmount = Convert.ToInt32(_client.ReadNode("ns=6;s=::Program:product.produced").Value);
            _status.ProduceTargetAmount = Convert.ToInt32(_client.ReadNode("ns=6;s=::Program:product.produce_amount").Value);
            _status.ProductGood = Convert.ToInt32(_client.ReadNode("ns=6;s=::Program:product.good").Value);
            _status.ProductBad = Convert.ToInt32(_client.ReadNode("ns=6;s=::Program:product.bad").Value);
            _status.ProdProcessedCount = Convert.ToInt32(_client.ReadNode("ns=6;s=::Program:Cube.Admin.ProdProcessedCount").Value);

            // INVENTORY
            _status.Barley = Convert.ToSingle(_client.ReadNode("ns=6;s=::Program:Inventory.Barley").Value);
            _status.Hops = Convert.ToSingle(_client.ReadNode("ns=6;s=::Program:Inventory.Hops").Value);
            _status.Malt = Convert.ToSingle(_client.ReadNode("ns=6;s=::Program:Inventory.Malt").Value);
            _status.Wheat = Convert.ToSingle(_client.ReadNode("ns=6;s=::Program:Inventory.Wheat").Value);
            _status.Yeast = Convert.ToSingle(_client.ReadNode("ns=6;s=::Program:Inventory.Yeast").Value);
            
            // // MAINTENANCE
            _status.MaintenanceCount = Convert.ToInt32(_client.ReadNode("ns=6;s=::Program:Maintenance.Counter").Value);
            _status.MaintenanceState = Convert.ToByte(_client.ReadNode("ns=6;s=::Program:Maintenance.State").Value);
            _status.MaintenanceTrigger = Convert.ToInt32(_client.ReadNode("ns=6;s=::Program:Maintenance.Trigger").Value);
            
            // // SENSOR DATA
            _status.Temperature = Convert.ToSingle(_client.ReadNode("ns=6;s=::Program:Cube.Status.Parameter[0].Value").Value);
            _status.Humidity = Convert.ToSingle(_client.ReadNode("ns=6;s=::Program:Cube.Status.Parameter[1].Value").Value);
            _status.Vibration = Convert.ToSingle(_client.ReadNode("ns=6;s=::Program:Cube.Status.Parameter[2].Value").Value);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }
}