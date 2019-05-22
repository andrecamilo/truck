using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using Truck.Domain.Register.Components.Queries;
using Truck.Domain.Register.Services.Queries;
using static Truck.Tests.TestUtils.TruckContextUtils;

namespace Components.Queries
{
    [TestFixture]
    public class GetTrucksQueryTests
    {
        [Test]
        public async Task Query_Execute_ReturnResults() {
            var context = GetInMemorySeededContext();
            var query = new GetTrucksQuery(context);

            var result = await query.ExecuteAsync(new GetTrucks());

            Assert.IsTrue(result.IsSuccess);
            Assert.GreaterOrEqual(3, result.Content.Count());
        }
    }
}
