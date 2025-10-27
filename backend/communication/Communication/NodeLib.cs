namespace communication.Communication
{
    public static class NodeLib
    {
        public static readonly string CurrentProduct = "ns=6;s=::Program:Cube.Admin.Parameter[0].Unit";
        public static readonly string ProdDefectiveCount = "ns=6;s=::Program:Cube.Admin.ProdDefectiveCount";
        public static readonly string ProdProccesedCount = "ns=6;s=::Program:Cube.Admin.ProdProcessedCount";
        public static readonly string StopReasonID = "ns=6;s=::Program:Cube.Admin.StopReason.ID";
        public static readonly string StopReasonValue = "ns=6;s=::Program:Cube.Admin.StopReason.Value";
        public static readonly string CmdChangeRequest = "ns=6;s=::Program:Cube.Command.CmdChangeRequest";
        public static readonly string CtrlCmd = "ns=6;s=::Program:Cube.Command.CntrlCmd";
        public static readonly string MachSpeed = "ns=6;s=::Program:Cube.Command.MachSpeed";
        public static readonly string BatchId = "ns=6;s=::Program:Cube.Command.Parameter[0].Value";
        public static readonly string ProductId = "ns=6;s=::Program:Cube.Command.Parameter[1].Value";
        public static readonly string AmountProductsPerBatch = "ns=6;s=::Program:Cube.Command.Parameter[2].Value";
        public static readonly string CurrentMachSpeed = "ns=6;s=::Program:Cube.Status.CurMachSpeed";
        public static readonly string StatusMachSpeed = "ns=6;s=::Program:Cube.Status.MachSpeed";
        public static readonly string StatusBatchId = "ns=6;s=::Program:Cube.Status.Parameter[0].Value";
        public static readonly string StatusCurrentAmount = "ns=6;s=::Program:Cube.Status.Parameter[1].Value";
        public static readonly string Humidity = "ns=6;s=::Program:Cube.Status.Parameter[2].Value";
        public static readonly string Temperature = "ns=6;s=::Program:Cube.Status.Parameter[3].Value";
        public static readonly string Vibration = "ns=6;s=::Program:Cube.Status.Parameter[3].Value";
        public static readonly string FillingInventory = "ns=6;s=::Program:FillingInventory";
        public static readonly string Barley = "ns=6;s=::Program:Inventory.Barley";
        public static readonly string Hops = "ns=6;s=::Program:Inventory.Hops";
        public static readonly string Malt = "ns=6;s=::Program:Inventory.Malt";
        public static readonly string Wheat = "ns=6;s=::Program:Inventory.Wheat";
        public static readonly string Yeast = "ns=6;s=::Program:Inventory.Yeast";
        public static readonly string MaintenanceCount = "ns=6;s=::Program:Maintenance.Counter";
        public static readonly string MaintenanceState = "ns=6;s=::Program:Maintenance.State";
        public static readonly string MaintenanceTrigger = "ns=6;s=::Program:Maintenance.Trigger";
        public static readonly string ProductBad = "ns=6;s=::Program:product.bad";
        public static readonly string ProductGood = "ns=6;s=::Program:product.good";
        public static readonly string ProduceAmount = "ns=6;s=::Program:product.produce_amount";
        public static readonly string ProducedAmount = "ns=6;s=::Program:product.produced";
        public static readonly string StateCurrent = "ns=6;s=::Program:Cube.Status.StateCurrent";
    }
}
