using System;
using System.Threading.Tasks;
using Truck.Domain.Register.Abstractions;
using Truck.Domain.Register.Services.Commands;
using static System.Console;
using static Truck.Presentation.Console.ConsoleInteractions;
using Truck.Domain.Register.Services.Queries;
using Truck.Infra.Helper;

namespace Truck.Presentation.Console.Modules
{
    public class DeleteTruckModule : IModule
    {
        readonly IFindTruckQuery findTruckQuery;
        readonly ICommand<DeleteTruck> deleteTruck;

        public DeleteTruckModule(
                IFindTruckQuery findTruckQuery,
                ICommand<DeleteTruck> deleteTruck) {
            this.findTruckQuery = findTruckQuery;
            this.deleteTruck = deleteTruck;
        }

        public async Task ExecuteAsync() {
            var Truck = await FindTruck(findTruckQuery);
            PrintTruck(Truck);
            Write("Deseja excluir o Caminhão? (S/N): ");

            var input = ReadKey();
            if (input.Key == ConsoleKey.S) {
                WriteLine();

                var command = new DeleteTruck(Truck.Chassis);
                var result = await deleteTruck.ExecuteAsync(command);
                if (result.IsSuccess) {
                    WriteLine("O Caminhão foi excluido com sucesso!");
                } else {
                    WriteLine($"Não foi possível excluir o Caminhão: {result.Message}");
                }
            }
        }
    }
}
