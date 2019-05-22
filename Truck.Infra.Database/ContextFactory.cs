using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Truck.Infra.Database
{
    class ContextFactory : IDesignTimeDbContextFactory<TruckContext>
    {
        public TruckContext CreateDbContext(string[] args) {
            var optionsBuilder = new DbContextOptionsBuilder<TruckContext>();
            optionsBuilder.UseSqlite("Data Source=truck_db.db");
            //optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=Truck;Trusted_Connection=True;");

            return new TruckContext(optionsBuilder.Options);
        }
    }
}
