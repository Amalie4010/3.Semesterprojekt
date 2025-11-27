using communication.Models;

namespace communication.Dtos
{
    public class GetCommandDto
    {
        public Guid Id { get; set; }
        public BeerTypes Type { get; set; }
        public int Amount { get; set; }
        public int Speed { get; set; }
        public int Progress { get; set; }

        public GetCommandDto(BeerTypes type, int amount, int speed, int progress)
        {
            Id = Guid.NewGuid();
            Type = type;
            Amount = amount;
            Speed = speed;
            Progress = progress;
        }
        public GetCommandDto(Command command, int progress = 0)
        {
            Id = command.Id;
            Type = command.Type;
            Amount = command.Amount;
            Speed = command.Speed;
            Progress = progress;
        }
    }
}
