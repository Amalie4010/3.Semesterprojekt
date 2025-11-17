using communication.Models;

namespace communication.Dtos
{
    public class PostCommandDto
    {
        public BeerTypes Type { get; set; }
        public int Amount { get; set; }
        public int Speed { get; set; }

        public PostCommandDto(BeerTypes type, int amount, int speed)
        {
            this.Type = type;
            this.Amount = amount;
            this.Speed = speed;
        }
    }
}
