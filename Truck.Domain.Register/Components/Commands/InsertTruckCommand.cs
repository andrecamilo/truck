using System;
using System.Threading.Tasks;
using Truck.Infra.Helper;
using Truck.Domain.Register.Services.Commands;
using Truck.Infra.Database;

namespace Truck.Domain.Register.Components.Commands
{
    public class InsertTruckCommand : ICommand<InsertTruck>
    {
        readonly TruckContext context;

        public InsertTruckCommand(TruckContext context) => this.context = context;

        public async Task<CommandResponse> ExecuteAsync(InsertTruck command) {
            try {
                context.Add(command.Truck);
                await context.SaveChangesAsync();
                return this.Success();
            } catch (Exception ex) {
                return this.Failure(ex.Message);
            }
        }
    }
}
