using communication.Communication;
using communication.Controllers;
using communication.Dtos;
using communication.Interfaces;
using communication.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;

namespace CommunicationTest
{
    internal class MachineControllerTest
    {
        Prod p;
        MachineController c;
        [SetUp]
        public void Setup()
        {
            p = new();
            c = new(p);
        }

        [Test]
        public async Task PostSamePowerState()
        {
            p.ps = PowerState.On;
            try
            {
                await c.Power(PowerState.On);
            } catch(Exception e)
            {
                Assert.Fail();
            }
            Assert.That(p.ps, Is.EqualTo(PowerState.On));
        }
        [Test]
        public async Task PostPowerState()
        {
            ObjectResult? res = null;
            p.ps = PowerState.Off;
            try
            {
                res = (ObjectResult)await c.Power(PowerState.On);
            }
            catch (Exception e)
            {
                Assert.Fail();
            }
            if (res == null)
                Assert.Fail();

            Assert.That(p.ps, Is.EqualTo(PowerState.On));
            Assert.That(res?.StatusCode, Is.EqualTo(200));
        }

        [Test]
        public void GetPowerStateTest()
        {
            p.ps = PowerState.On;
            OkObjectResult? response = (OkObjectResult?)c.GetPower().Result;
            if (response == null)
                Assert.Fail();
            var result = response!.Value;
            Assert.That(result, Is.EqualTo(PowerState.On));
        }

        [Test]
        public void SendCommandWhenPSIsOff()
        {
            p.ps = PowerState.Off;
            PostCommandDto cdto = new(BeerTypes.Ale, 1, 1);
            ObjectResult res = (ObjectResult)c.SendCommand(cdto);
            Assert.That(res.StatusCode, Is.EqualTo(429));  
        }
        [Test]
        public void PostSendCommand()
        {
            p.ps = PowerState.On;
            var cdto = new PostCommandDto(BeerTypes.Pilsner, 100, 100);
            var res = (ObjectResult)c.SendCommand(cdto);
            Assert.That(res.StatusCode, Is.EqualTo(200));
        }
        [Test]
        public void DeleteNonExistentCommand()
        {
            var co = new Command(BeerTypes.Ale, 1, 1);
            var res = c.DeleteCommand(co.Id);
            Assert.That(((ObjectResult)res).StatusCode, Is.EqualTo(400));
        }
        [Test]
        public void DeleteCommand()
        {
            p.ps = PowerState.On;
            var cdto = new PostCommandDto(BeerTypes.Ale, 1, 1);
            OkObjectResult result = (OkObjectResult)c.SendCommand(cdto);
            Command? command = (Command?)result?.Value;
            Assert.That(command, Is.Not.Null);
            OkResult? res = (OkResult?)c.DeleteCommand(command.Id);
            Assert.That(res, Is.Not.Null);
            Assert.That(res.StatusCode, Is.EqualTo(200));
        }
        [Test]
        public void GetCurrentCommand()
        {
            p.ps = PowerState.On;
            PostCommandDto cdto = new(BeerTypes.Ale, 1, 1);
            OkObjectResult? res = (OkObjectResult?)c.SendCommand(cdto);
            Command? co = (Command?)res?.Value;
            Assert.That(co, Is.Not.Null);
            var res2 = c.GetCommandsCurrent().Value;
            Assert.That(res2?[0]?.Id, Is.EqualTo(co.Id));
        }
        [Test]
        public void GetEmptyCurrentCommand()
        {
            p.ps = PowerState.On;
            GetCommandDto?[]? res = c.GetCommandsCurrent().Value;
            Assert.That(res, Is.Not.Null);
            Assert.That(res.Length, Is.EqualTo(0));
        }
    }
    public class Prod : IProduction
    {
        public PowerState ps;
        public List<Command> deletedCommands = new();
        public List<Command> commands = new();
        public static Production GetInstance()
        {
            throw new NotImplementedException();
        }

        public static int GetPublishInterval()
        {
            throw new NotImplementedException();
        }

        public static int GetTimeout()
        {
            throw new NotImplementedException();
        }

        public bool DeleteCommand(Guid id)
        {
            Command? c = commands.FirstOrDefault(c => c.Id == id);
            if (c == null)
                return false;
            return commands.Remove(c);
            
        }

        public IEnumerable<string> GetAllMachines()
        {
            throw new NotImplementedException();
        }

        public Command?[] GetCurrentCommands()
        {
            return commands.ToArray();
        }

        public int[] GetProgress()
        {
            int[] res = new int[commands.Count];
            for (int i = 0; i < res.Length; i++)
            {
                res[i] = 0;
            }
            return res;
        }

        public Command[] GetQueue()
        {
            throw new NotImplementedException();
        }

        public PowerState GetState()
        {
            return ps;
        }

        public MachineStatus GetStatus(string connectionString)
        {
            throw new NotImplementedException();
        }

        public void MakeNewMachine(string connectionString)
        {
            throw new NotImplementedException();
        }

        public void NewCommand(Command command)
        {
            commands.Add(command);
        }

        public Task<PowerState> Power(PowerState powerState)
        {
            ps = powerState;
            Task<PowerState> tps = new(() => { return powerState; });
            tps.Start();
            return tps;
        }
    }
}
