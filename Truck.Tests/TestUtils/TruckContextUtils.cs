using Microsoft.EntityFrameworkCore;
using Truck.Infra.Database;

namespace Truck.Tests.TestUtils
{
    public static class TruckContextUtils
    {
        public static TruckContext GetInMemoryEmptyContext() {
            var options = new DbContextOptionsBuilder<TruckContext>()
                .UseSqlite("Data Source=truck_db.db")
                .Options;

            var context = new TruckContext(options);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            return context;
        }

        public static TruckContext GetInMemorySeededContext() {
            var context = GetInMemoryEmptyContext();

            context.AddRange(
                new Infra.Database.Entities.Truck { Chassis = "TEST1", ColorId = 1, Model = Truck.Infra.Database.Models.TruckModel.FH },
                new Infra.Database.Entities.Truck { Chassis = "TEST2", ColorId = 2, Model = Truck.Infra.Database.Models.TruckModel.FH },
                new Infra.Database.Entities.Truck { Chassis = "TEST3", ColorId = 3, Model = Truck.Infra.Database.Models.TruckModel.FM }
            );
            context.SaveChanges();

            return context;
        }
    }
}
