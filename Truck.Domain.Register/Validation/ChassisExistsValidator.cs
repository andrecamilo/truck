using System.Linq;
using Truck.Infra.Database;

namespace Truck.Domain.Register.Validation
{
    /// <summary>
    /// Valida se a entrada é um código de chassi já cadastrado no repositório.
    /// </summary>
    public class ChassisExistsValidator : IValidator
    {
        readonly TruckContext context;
        readonly string errorMessage;

        public ChassisExistsValidator(TruckContext context, string errorMessage) {
            this.context = context;
            this.errorMessage = errorMessage;
        }

        public (bool, string) Validate(string value) {
            var exists = context.Truck.Any(v => v.Chassis == value);
            return (!exists, exists ? errorMessage : "Não foi possível executar o procedimento");
        }
    }
}
