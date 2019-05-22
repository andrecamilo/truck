using Truck.Infra.Helper;

namespace Truck.Domain.Register.Services.Queries
{
    public interface IFindTruckQuery : IQuery<string, Truck.Infra.Database.Entities.Truck>
    {
    }
}
