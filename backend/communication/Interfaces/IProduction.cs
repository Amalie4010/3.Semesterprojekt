using communication.Communication;
using communication.Models;
using System.Threading;

namespace communication.Interfaces
{
    public interface IProduction
    {
        public abstract static Production GetInstance();
        public Task<PowerState> Power(PowerState powerState);
        public void NewCommand(Command command);
        public bool DeleteCommand(Guid id);
        public Command?[] GetCurrentCommands();
        public int[] GetProgress();

        public abstract static int GetTimeout();
        public abstract static int GetPublishInterval();
        public PowerState GetState();
    }
}
