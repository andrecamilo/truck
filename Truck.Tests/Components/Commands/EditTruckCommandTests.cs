using System;
using System.Threading.Tasks;
using NUnit.Framework;
using Truck.Domain.Register.Components.Commands;
using Truck.Domain.Register.Services.Commands;
using static Truck.Tests.TestUtils.TruckContextUtils;

namespace Components.Commands
{
    [TestFixture]
    public class EditTruckCommandTests
    {
        [Test]
        public async Task Command_Execute_ShouldEdit() {
            var context = GetInMemorySeededContext();
            var update = new EditTruckCommand(context);
            var command = new EditTruck("TEST1", 2, 3, 4);

            await update.ExecuteAsync(command);

            var updatedTruck = context.Truck.Find("TEST1");
            Assert.AreEqual(2, updatedTruck.ColorId);
        }
    }
}
