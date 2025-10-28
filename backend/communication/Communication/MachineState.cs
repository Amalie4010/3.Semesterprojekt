namespace communication.Communication
{
    public static class MachineState
    {
        public static readonly int Idle = 4;
        public static readonly int Held = 11;
        public static readonly int Completed = 17;
        public static readonly int Execute = 6;
        public static readonly int Stopped = 2;
        public static readonly int Aborted = 9;
    }
}
