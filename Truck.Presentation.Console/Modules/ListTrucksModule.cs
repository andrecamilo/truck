using System.Linq;
using System.Threading.Tasks;
using Truck.Domain.Register.Abstractions;
using Truck.Domain.Register.Services.Queries;
using static System.Console;

namespace Truck.Presentation.Console.Modules
{
    public class ListTrucksModule : IModule
    {
        readonly IGetTrucksQuery getTrucks;

        public ListTrucksModule(IGetTrucksQuery getTrucks) => this.getTrucks = getTrucks;

        public async Task ExecuteAsync() {

            WriteLine();
            WriteLine();
            WriteLine("Lista de Caminhãos cadastrados: ");

            var query = new GetTrucks();
            var trucksResult = await getTrucks.ExecuteAsync(query);
            if (!trucksResult.IsSuccess) {
                WriteLine($"Não foi possível obter a lista de Caminhãos: {trucksResult.Message}");
                return;
            }

            trucksResult.Content
                .ToList()
                .ForEach(ConsoleInteractions.PrintTruck);
        }
    }
}
