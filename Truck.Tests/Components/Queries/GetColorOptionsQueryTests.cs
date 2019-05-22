using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;
using Truck.Domain.Register.Components.Queries;
using Truck.Domain.Register.Services.Queries;
using static Truck.Tests.TestUtils.TruckContextUtils;

namespace Components.Queries
{
    [TestFixture]
    public class GetColorOptionsQueryTests
    {
        [Test]
        public async Task Query_Execute_ReturnResults() {
            var context = GetInMemorySeededContext();
            var query = new GetColorOptionsQuery(context);

            var result = await query.ExecuteAsync(new GetColorOptions());

            Assert.IsTrue(result.IsSuccess);
            Assert.IsTrue(result.Content.Count() > 0);
        }
    }
}
