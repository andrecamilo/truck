using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Internal;
using NUnit.Framework;
using Truck.Domain.Register.Components.Commands;
using Truck.Domain.Register.Services.Commands;
using static Truck.Tests.TestUtils.TruckContextUtils;

namespace Components.Commands
{
    public class DeleteTruckCommandTests
    {
        [Test]
        public async Task Command_Execute_ShouldDelete() {
            var context = GetInMemorySeededContext();

            var delete = new DeleteTruckCommand(context);
            var command = new DeleteTruck("TEST1");

            await delete.ExecuteAsync(command);

            var exists = context.Truck.Any(v => v.Chassis == "TEST1");
            Assert.IsFalse(exists);
        }
    }
}
