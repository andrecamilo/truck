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
    public class GetColorOptionsQuery : IGetColorOptionsQuery
    {
        readonly TruckContext context;

        public GetColorOptionsQuery(TruckContext context) => this.context = context;

        public async Task<QueryResponse<IEnumerable<ColorOption>>> ExecuteAsync(GetColorOptions query) {
            var content = await context.Color.ToListAsync();
            return this.Result(content);
        }
    }
}
