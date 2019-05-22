using System.Collections.Generic;
using Truck.Infra.Helper;

namespace Truck.Domain.Register.Services.Queries
{
    public interface IGetTrucksQuery : IQuery<GetTrucks, IEnumerable<Truck.Infra.Database.Entities.Truck>>
    {
    }

    public class GetTrucks
    {
    }
}
