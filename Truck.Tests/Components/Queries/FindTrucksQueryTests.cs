using System;
using System.Threading.Tasks;
using NUnit.Framework;
using Truck.Domain.Register.Components.Queries;
using static Truck.Tests.TestUtils.TruckContextUtils;

namespace Components.Queries
{
    [TestFixture]
    public class FindTrucksQueryTests
    {
        [Test]
        public async Task Query_FindWithValidChassi_ReturnTrucks() {
            var context = GetInMemorySeededContext();
            var query = new FindTruckQuery(context);

            var result = await query.ExecuteAsync("TEST1");

            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual("TEST1", result.Content.Chassis);
        }

        [Test]
        public async Task Query_FindWithInvalidChassi_Fails() {
            var context = GetInMemorySeededContext();
            var query = new FindTruckQuery(context);

            var result = await query.ExecuteAsync("TEST");

            Assert.IsFalse(result.IsSuccess);
        }
    }
}
