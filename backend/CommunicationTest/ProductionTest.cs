using communication.Communication;
using communication.Controllers;
using communication.Interfaces;
using communication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunicationTest
{
    internal class ProductionTest
    {
        CommandQueue c = new();
        TestMachine ms;
        Production p = Production.GetInstance();

        [SetUp]
        public void Setup()
        {
            ms = new TestMachine("opc.tcp://127.0.0.1:4840", c);
            p.MakeNewMachine(ms);
        }

        [Test]
        public async Task TestPowerOn()
        {
            ms.shouldFailConnect = false;
            try
            {
                var pt = await p.Power(PowerState.On);
                Assert.That(pt, Is.EqualTo(PowerState.On));
            } catch
            {
                Assert.Fail();
                return;
            }
        }
        [Test]
        public async Task TestPowerOff()
        {
            ms.shouldFailConnect = false;
            _ = await p.Power(PowerState.On);
            
            try
            {
                var pt = await p.Power(PowerState.Off);
                Assert.That(pt, Is.EqualTo(PowerState.Off));
            }
            catch
            {
                Assert.Fail();
                return;
            }
        }
        [Test]
        public async Task TestPowerTimeout()
        {
            ms.shouldFailConnect = true;
            try
            {
                var pt = await p.Power(PowerState.On);
                Assert.Fail(); // Should never reach here
            }
            catch(Exception e)
            {
                if (e is TimeoutException && !ms.isConnected())
                {
                    Assert.Pass();
                    return;
                }
                Assert.Fail();
                return;
            }
        }
    }
    class TestMachine : IMachine
    {
        public string constr;
        public CommandQueue queue;
        public bool shouldFailConnect;
        PowerState ps = PowerState.Off;
        public TestMachine(string constr, CommandQueue queue)
        {
            this.constr = constr;
            this.queue = queue;
        }
        public Task<PowerState> Connect(PowerState powerState)
        {
            Task<PowerState> t;
            if (!shouldFailConnect)
            {
                ps = powerState;
                t = Task.FromResult(ps);
            } else
            {
                shouldFailConnect = !shouldFailConnect;
                throw (new TimeoutException());
            }
            return t;
        }

        public string GetConnectionString()
        {
            throw new NotImplementedException();
        }

        public Command? GetCurrentCommand()
        {
            throw new NotImplementedException();
        }

        public int GetProgress()
        {
            throw new NotImplementedException();
        }

        public MachineStatus GetStatus()
        {
            throw new NotImplementedException();
        }

        public bool isConnected()
        {
            return ps == PowerState.On;
        }

        public void Stop()
        {
            return;
        }
    }
}
