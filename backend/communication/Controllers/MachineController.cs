using communication.Communication;
using communication.Dtos;
using communication.Interfaces;
using communication.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace communication.Controllers
{
    [ApiController]
    [Route("api/communication")]
    public class MachineController : ControllerBase
    {
        private readonly IProduction _production;

        public MachineController(IProduction? production = null)
        {
            _production = production ?? Production.GetInstance();
        }

        [HttpPost("power")]
        public async Task<IActionResult> Power([FromBody] PowerState power_state)
        {
            if (_production.GetState() == power_state)
                return Ok(_production.GetState());

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
            var res = _production.GetState();
            return Ok(res);
        }

        [HttpPost("command")]
        public IActionResult SendCommand([FromBody] PostCommandDto commandDto)
        {
            if (_production.GetState() == PowerState.Off)
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

        [HttpPost("machine")]
        public IActionResult MakeMachine([FromBody] string connectionString)
        {
            _production.MakeNewMachine(connectionString);
            return Ok(new { message = "Machine added", connectionString });
        }

        [HttpGet("machines")]
        public IActionResult GetMachines()
        {
            return Ok(_production.GetAllMachines());
        }
    }
}
