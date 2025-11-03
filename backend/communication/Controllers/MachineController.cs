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
        [HttpGet("power")]
        public ActionResult<PowerState> GetPower()
        {
            return Ok(_production.State);
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
            bool deleted = _production.DeleteCommand(id);
            if (!deleted)
                return BadRequest($"The specified command either doesn't exist or has already been completed");

            return Ok();

        }
        [HttpGet("command/current")]
        public ActionResult<Command?[]> GetCommandCurrent()
        {
            return _production.GetCurrentCommands();
        }
        [HttpGet("command/current/progress")]
        public ActionResult<int[]> GetCommandProgress()
        {
            return _production.GetProgress();
        }
    }
}
