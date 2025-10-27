namespace communication.Models
{
    public class MachineStatus
    {
        private readonly static MachineStatus instance = new();

        public PowerState PowerState { get; set; } = PowerState.Off;
        public int MaintanenceCount { get; set; } = 0;
        public int BarleyCount { get; set; } = 0;
        public int HopsCount { get; set; } = 0;
        public int MaltCount { get; set; } = 0;
        public int WheatCount { get; set; } = 0;
        public int YeastCount { get; set; } = 0;

        private MachineStatus(){}
        public static MachineStatus GetInstance()
        {
            return instance;
        }
    }
}
