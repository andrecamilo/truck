using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Truck.Infra.Database.Entities;
using Truck.Infra.Helper;
using Truck.Domain.Register.Services.Queries;
using Truck.Infra.Database;
using Microsoft.EntityFrameworkCore;

namespace Truck.Domain.Register.Components.Queries
{
    public class GetTrucksQuery : IGetTrucksQuery
    {
        readonly TruckContext context;

        public GetTrucksQuery(TruckContext context) => this.context = context;

        public async Task<QueryResponse<IEnumerable<Truck.Infra.Database.Entities.Truck>>> ExecuteAsync(GetTrucks query) {
            var content = await context.Truck
                .ToListAsync();
            return this.Result(content);
        }
    }
}
