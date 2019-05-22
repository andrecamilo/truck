using System;
using System.Threading.Tasks;
using Truck.Infra.Helper;
using Truck.Domain.Register.Services.Commands;
using Truck.Infra.Database;
using Truck.Domain.Register.Services.Queries;

namespace Truck.Domain.Register.Components.Commands
{
    public class EditTruckCommand : ICommand<EditTruck>
    {
        readonly TruckContext context;

        public EditTruckCommand(TruckContext context) => this.context = context;

        public async Task<CommandResponse> ExecuteAsync(EditTruck command) {
            var Truck = await context.Truck.FindAsync(command.Chassis);
            if (Truck == null)
                return this.Failure("Caminhão não encontrado.");

            try {
                Truck.ColorId = command.ColorId;
                Truck.ManufactureYear = command.ManuYear;
                Truck.ModelYear = command.ModelYear;
                await context.SaveChangesAsync();
                return this.Success();
            } catch (Exception ex) {
                return this.Failure(ex.Message);
            }
        }
    }
}
