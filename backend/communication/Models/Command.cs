using communication.Dtos;

namespace communication.Models
{
    public class Command
    {
        public Guid Id {  get; set; }
        public BeerTypes Type { get; set; }
        public int Amount { get; set; }
        public int Speed { get; set; }

        public Command(BeerTypes type, int amount, int speed)
        {
            this.Id = Guid.NewGuid();
            this.Type = type;
            this.Amount = amount;
            this.Speed = speed;
        }
        public Command(PostCommandDto dto)
        {
            Id = Guid.NewGuid();
            Type = dto.Type;
            Amount = dto.Amount;
            Speed = dto.Speed;
        }
    }
}
