using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Truck.Domain.Register.Services.Queries;
using Truck.Infra.Database;
using Truck.Infra.Helper;

namespace Truck.Domain.Register.Components.Queries
{
    public class FindTruckQuery : IFindTruckQuery
    {
        private readonly TruckContext context;

        public FindTruckQuery(TruckContext context) => this.context = context;

        public async Task<QueryResponse<Truck.Infra.Database.Entities.Truck>> ExecuteAsync(string chassis) {
            var Truck = await context.Truck
                .Include(v => v.Color)
                .FirstOrDefaultAsync(v => v.Chassis == chassis);

            if (Truck != null)
                return this.Result(Truck);

            return this.ResultError("Caminhão não encontrado.");
        }
    }
}
