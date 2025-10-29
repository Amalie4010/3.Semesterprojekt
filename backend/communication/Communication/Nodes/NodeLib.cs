namespace communication.Communication.Nodes
{
    public static class NodeLib
    {
        public static readonly OpcNode<float> CurrentProduct = new("ns=6;s=::Program:Cube.Admin.Parameter[0].Value");
        public static readonly OpcNode<int> ProdDefectiveCount = new("ns=6;s=::Program:Cube.Admin.ProdDefectiveCount");
        public static readonly OpcNode<int> ProdProccesedCount = new("ns=6;s=::Program:Cube.Admin.ProdProcessedCount");
        public static readonly OpcNode<int> StopReasonID = new("ns=6;s=::Program:Cube.Admin.StopReason.Id");
        public static readonly OpcNode<int> StopReasonValue = new("ns=6;s=::Program:Cube.Admin.StopReason.Value");
        public static readonly OpcNode<bool> CmdChangeRequest = new("ns=6;s=::Program:Cube.Command.CmdChangeRequest");
        public static readonly OpcNode<int> CtrlCmd = new("ns=6;s=::Program:Cube.Command.CntrlCmd");
        public static readonly OpcNode<float> MachSpeed = new("ns=6;s=::Program:Cube.Command.MachSpeed");
        public static readonly OpcNode<float> BatchId = new("ns=6;s=::Program:Cube.Command.Parameter[0].Value");
        public static readonly OpcNode<float> ProductId = new("ns=6;s=::Program:Cube.Command.Parameter[1].Value");
        public static readonly OpcNode<float> AmountProductsPerBatch = new("ns=6;s=::Program:Cube.Command.Parameter[2].Value");
        public static readonly OpcNode<float> CurrentMachSpeed = new("ns=6;s=::Program:Cube.Status.CurMachSpeed");
        public static readonly OpcNode<float> StatusMachSpeed = new("ns=6;s=::Program:Cube.Status.MachSpeed");
        public static readonly OpcNode<float> StatusBatchId = new("ns=6;s=::Program:Cube.Status.Parameter[0].Value");
        public static readonly OpcNode<float> StatusCurrentAmount = new("ns=6;s=::Program:Cube.Status.Parameter[1].Value");
        public static readonly OpcNode<float> Humidity = new("ns=6;s=::Program:Cube.Status.Parameter[2].Value");
        public static readonly OpcNode<float> Temperature = new("ns=6;s=::Program:Cube.Status.Parameter[3].Value");
        public static readonly OpcNode<float> Vibration = new("ns=6;s=::Program:Cube.Status.Parameter[3].Value");
        public static readonly OpcNode<int> StateCurrent = new("ns=6;s=::Program:Cube.Status.StateCurrent");
        public static readonly OpcNode<bool> FillingInventory = new("ns=6;s=::Program:FillingInventory");
        public static readonly OpcNode<float> Barley = new("ns=6;s=::Program:Inventory.Barley");
        public static readonly OpcNode<float> Hops = new("ns=6;s=::Program:Inventory.Hops");
        public static readonly OpcNode<float> Malt = new("ns=6;s=::Program:Inventory.Malt");
        public static readonly OpcNode<float> Wheat = new("ns=6;s=::Program:Inventory.Wheat");
        public static readonly OpcNode<float> Yeast = new("ns=6;s=::Program:Inventory.Yeast");
        public static readonly OpcNode<int> MaintenanceCount = new("ns=6;s=::Program:Maintenance.Counter");
        public static readonly OpcNode<byte> MaintenanceState = new("ns=6;s=::Program:Maintenance.State");
        public static readonly OpcNode<int> MaintenanceTrigger = new("ns=6;s=::Program:Maintenance.Trigger");
        public static readonly OpcNode<int> ProductBad = new("ns=6;s=::Program:product.bad");
        public static readonly OpcNode<int> ProductGood = new("ns=6;s=::Program:product.good");
        public static readonly OpcNode<int> ProduceAmount = new("ns=6;s=::Program:product.produce_amount");
        public static readonly OpcNode<int> ProducedAmount = new("ns=6;s=::Program:product.produced");
    }
}
