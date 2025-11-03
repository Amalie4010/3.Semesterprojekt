namespace communication.Models
{
    public class MachineStatus
    {
        /***********
         THIS CLASS IS A MODEL FOR THE MACHINES STATUS AND HOLDS ALL DATA FOR THE RELEVANT STATUS OF THE MACHINE
         ***********/

        public PowerState PowerState { get; set; } = PowerState.Off;
        
        // MACHINE STATUS
        public int StateCurrent { get; set; } = 0;
        public float CurrentMachSpeed { get; set; } = 0;
        public int StopReasonID { get; set; } = 0;
        public int StopReasonValue { get; set; } = 0;

        // PRODUCTION 
        public float BatchId { get; set; } = 0;
        public float CurrentProduct { get; set; } = 0;
        public int ProducedAmount { get; set; } = 0;
        public int ProduceTargetAmount { get; set; } = 0;
        public int ProductGood { get; set; } = 0;
        public int ProductBad { get; set; } = 0;
        public int ProdProcessedCount { get; set; } = 0;
        
        // INVENTORY
        public float Barley { get; set; } = 0;
        public float Hops { get; set; } = 0;
        public float Malt { get; set; } = 0;
        public float Wheat { get; set; } = 0;
        public float Yeast { get; set; } = 0;
        public bool FillingInventory { get; set; } = false;


        // MAINTENANCE STATUS
        public int MaintenanceCount { get; set; } = 0;
        public byte MaintenanceState { get; set; } = 0;
        public int MaintenanceTrigger { get; set; } = 0;

        // SENSOR DATA
        public float Temperature { get; set; } = 0;
        public float Humidity { get; set; } = 0;
        public float Vibration { get; set; } = 0;
        
        private MachineStatus(){}
    }
}
