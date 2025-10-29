namespace communication.Communication
{
    public static class NodeLib
    {
        public const string CurrentProduct = "ns=6;s=::Program:Cube.Admin.Parameter[0].Unit";
        public const string ProdDefectiveCount = "ns=6;s=::Program:Cube.Admin.ProdDefectiveCount";
        public const string ProdProccesedCount = "ns=6;s=::Program:Cube.Admin.ProdProcessedCount";
        public const string StopReasonID = "ns=6;s=::Program:Cube.Admin.StopReason.ID";
        public const string StopReasonValue = "ns=6;s=::Program:Cube.Admin.StopReason.Value";
        public const string CmdChangeRequest = "ns=6;s=::Program:Cube.Command.CmdChangeRequest";
        public const string CtrlCmd = "ns=6;s=::Program:Cube.Command.CntrlCmd";
        public const string MachSpeed = "ns=6;s=::Program:Cube.Command.MachSpeed";
        public const string BatchId = "ns=6;s=::Program:Cube.Command.Parameter[0].Value";
        public const string ProductId = "ns=6;s=::Program:Cube.Command.Parameter[1].Value";
        public const string AmountProductsPerBatch = "ns=6;s=::Program:Cube.Command.Parameter[2].Value";
        public const string CurrentMachSpeed = "ns=6;s=::Program:Cube.Status.CurMachSpeed";
        public const string StatusMachSpeed = "ns=6;s=::Program:Cube.Status.MachSpeed";
        public const string StatusBatchId = "ns=6;s=::Program:Cube.Status.Parameter[0].Value";
        public const string StatusCurrentAmount = "ns=6;s=::Program:Cube.Status.Parameter[1].Value";
        public const string Humidity = "ns=6;s=::Program:Cube.Status.Parameter[2].Value";
        public const string Temperature = "ns=6;s=::Program:Cube.Status.Parameter[3].Value";
        public const string Vibration = "ns=6;s=::Program:Cube.Status.Parameter[3].Value";
        public const string FillingInventory = "ns=6;s=::Program:FillingInventory";
        public const string Barley = "ns=6;s=::Program:Inventory.Barley";
        public const string Hops = "ns=6;s=::Program:Inventory.Hops";
        public const string Malt = "ns=6;s=::Program:Inventory.Malt";
        public const string Wheat = "ns=6;s=::Program:Inventory.Wheat";
        public const string Yeast = "ns=6;s=::Program:Inventory.Yeast";
        public const string MaintenanceCount = "ns=6;s=::Program:Maintenance.Counter";
        public const string MaintenanceState = "ns=6;s=::Program:Maintenance.State";
        public const string MaintenanceTrigger = "ns=6;s=::Program:Maintenance.Trigger";
        public const string ProductBad = "ns=6;s=::Program:product.bad";
        public const string ProductGood = "ns=6;s=::Program:product.good";
        public const string ProduceAmount = "ns=6;s=::Program:product.produce_amount";
        public const string ProducedAmount = "ns=6;s=::Program:product.produced";
        public const string StateCurrent = "ns=6;s=::Program:Cube.Status.StateCurrent";
    }
}
