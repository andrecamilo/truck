using System;
using System.Threading.Tasks;
using Truck.Domain.Register.Abstractions;
using static Truck.Presentation.Console.ConsoleInteractions;
using Truck.Domain.Register.Services.Queries;

namespace Truck.Presentation.Console.Modules
{
    public class FindTruckModule : IModule
    {
        readonly IFindTruckQuery findTruckQuery;

        public FindTruckModule(IFindTruckQuery findTruckQuery) {
            this.findTruckQuery = findTruckQuery;
        }

        public async Task ExecuteAsync() {
            var Truck = await FindTruck(findTruckQuery);
            PrintTruck(Truck);
        }
    }
}
