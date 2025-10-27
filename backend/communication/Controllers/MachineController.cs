using communication.Dtos;
using communication.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace communication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MachineController : ControllerBase
    {
        public MachineController()
        {
        }

        [HttpPost("power")]
        public IActionResult Power([FromBody] PowerState power_state)
        {
            return Ok(power_state);
        }
        [HttpPost("command")]
        public IActionResult SendCommand([FromBody] PostCommandDto commandDto)
        {
            Command command = new(commandDto);
            return Ok(command);
        }
        [HttpDelete("command/{id}")]
        public IActionResult DeleteCommand(Guid id)
        {
            return Ok();
        }
        [HttpGet("command/{id}")]
        public ActionResult<Command> GetCommand(Guid id)
        {
            Command command = new Command(BeerTypes.Pilsner, 1, 1);
            command.Id = id;
            return Ok(command);
        }
        [HttpGet("command/current")]
        public ActionResult<Command> GetCommandCurrent()
        {
            Command command = new Command(BeerTypes.Pilsner, 1, 1);
            return command;
        }
        [HttpGet("command/{id}/progress")]
        public ActionResult<int> GetCommandProgress(Guid id)
        {
            return 1;
        }
        [HttpGet("status")]
        public ActionResult<MachineStatus> GetMachineStatus()
        {
            return MachineStatus.GetInstance();
        }
    }
}
