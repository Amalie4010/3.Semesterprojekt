using communication.Communication;
using communication.Dtos;
using communication.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace communication.Controllers
{
    [ApiController]
    [Route("api/communication")]
    public class MachineController : ControllerBase
    {
        private readonly Production _production;
        public MachineController()
        {
            _production = Production.GetInstance();
        }

        [HttpPost("power")]
        public async Task<IActionResult> Power([FromBody] PowerState power_state)
        {
            if (_production.State == power_state)
                return Ok(_production.State);

            var ps = await _production.Power(power_state);
            if (ps == power_state)
            {
                return Ok(ps);
            } else if (power_state == PowerState.On)
            {
                return StatusCode(500, "Could not establish connection with machine(s)");
            } else
            {
                return StatusCode(500, "Something went wrong whilst disconnecting");
            }
        }

        [HttpPost("command")]
        public IActionResult SendCommand([FromBody] PostCommandDto commandDto)
        {
            if (_production.State == PowerState.Off)
            {
                return StatusCode(429, "Production must be powered on before using this endpoint");
            }

            Command command = new(commandDto);
            _production.NewCommand(command);
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
