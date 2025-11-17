using communication.Models;
using System.Diagnostics.Eventing.Reader;

namespace communication.Communication
{
    public class CommandQueue
    {
        private Queue<Command> queue = new();
        private TaskCompletionSource queueTcs = new();
        private HashSet<Guid> queuedCommandIds = new();
        private HashSet<Guid> toBeDeletedCommandIds = new();
        public int Count => queue.Count;
        public CommandQueue()
        {
            
        }
        public bool Delete(Guid id)
        {
            if (queuedCommandIds.Remove(id))
            {
                return toBeDeletedCommandIds.Add(id);
            }
            return false;
        }
        public void Enqueue(Command command)
        {
            queue.Enqueue(command);
            queuedCommandIds.Add(command.Id);
            ResetTcs();
        }
        public async Task<Command> Dequeue()
        {
            while (true)
            {
                lock(queue)
                {   
                    if (queue.Count > 0)
                        return SafeDequeue();
                }
                await queueTcs.Task;
            }
        }
        private Command SafeDequeue()
        {
            Command command = queue.Dequeue();
            while (toBeDeletedCommandIds.Contains(command.Id))
            {
                toBeDeletedCommandIds.Remove(command.Id);
                command = queue.Dequeue();
            }
            return command;
        }
        private void ResetTcs()
        {
            var oldTcs = queueTcs;
            queueTcs = new();
            oldTcs.SetResult();
        }
    }
}
