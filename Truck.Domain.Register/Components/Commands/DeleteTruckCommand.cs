using System;
using System.Linq;
using System.Threading.Tasks;
using Truck.Domain.Register.Services.Commands;
using Truck.Infra.Database;
using Truck.Infra.Helper;

namespace Truck.Domain.Register.Components.Commands
{
    public class DeleteTruckCommand : ICommand<DeleteTruck>
    {
        readonly TruckContext context;

        public DeleteTruckCommand(TruckContext context) => this.context = context;

        public async Task<CommandResponse> ExecuteAsync(DeleteTruck command) {
            var Truck = context.Truck.FirstOrDefault(v => v.Chassis == command.Chassis);
            if (Truck == null)
                return this.Failure("Caminhão não encontrado.");

            try {
                context.Remove(Truck);
                await context.SaveChangesAsync();
                return this.Success();
            } catch (Exception ex) {
                return this.Failure(ex.Message);
            }
        }
    }
}
