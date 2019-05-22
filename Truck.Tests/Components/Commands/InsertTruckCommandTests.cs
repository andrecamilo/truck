using System;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using Truck.Domain.Register.Components.Commands;
using Truck.Infra.Database.Entities;
using Truck.Infra.Database.Models;
using Truck.Domain.Register.Services.Commands;
using static Truck.Tests.TestUtils.TruckContextUtils;

namespace Components.Commands
{
    [TestFixture]
    public class InsertTruckCommandTests
    {
        [Test]
        public async Task Command_Execute_ShouldInsert() {
            var context = GetInMemoryEmptyContext();
            var Truck = new Truck.Infra.Database.Entities.Truck {
                Chassis = "TESTE2",
                ColorId = 1,
                Model = TruckModel.FM
            };
            var command = new InsertTruck(Truck);
            var insert = new InsertTruckCommand(context);
            var commandResult = await insert.ExecuteAsync(command);

            Assert.IsTrue(commandResult.IsSuccess);
            var insertedTruck = context.Truck.Last();
            Assert.AreEqual(Truck.Chassis, insertedTruck.Chassis);
        }

        [Test]
        public async Task Command_DuplicateKey_ShouldFail() {
            var context = GetInMemoryEmptyContext();
            var Truck = new Truck.Infra.Database.Entities.Truck {
                Chassis = "TEST1",
                ColorId = 1,
                Model = TruckModel.FM,
            };
            var command = new InsertTruck(Truck);
            var insert = new InsertTruckCommand(context);

            await insert.ExecuteAsync(command);
            var commandResult = await insert.ExecuteAsync(command);

            Assert.IsFalse(commandResult.IsSuccess);
        }
    }
}
