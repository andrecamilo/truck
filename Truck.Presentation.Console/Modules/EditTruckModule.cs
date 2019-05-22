using System;
using System.Threading.Tasks;
using Truck.Domain.Register.Abstractions;
using Truck.Domain.Register.Services.Queries;
using static System.Console;
using static Truck.Presentation.Console.ConsoleInteractions;
using Truck.Domain.Register.Services.Commands;
using Truck.Infra.Helper;
using System.Collections.Generic;
using Truck.Domain.Register.Validation;

namespace Truck.Presentation.Console.Modules
{
    public class EditTruckModule : IModule
    {
        readonly IFindTruckQuery findTruckQuery;
        readonly IGetColorOptionsQuery getColorOptions;
        readonly ICommand<EditTruck> editTruck;

        public EditTruckModule(
                IFindTruckQuery findTruckQuery,
                IGetColorOptionsQuery getColorOptions,
                ICommand<EditTruck> editTruck) {
            this.findTruckQuery = findTruckQuery;
            this.getColorOptions = getColorOptions;
            this.editTruck = editTruck;
        }

        public async Task ExecuteAsync() {
            var Truck = await FindTruck(findTruckQuery);
            PrintTruck(Truck);

            WriteLine();
            WriteLine("Você pode modificar a cor do Caminhão.");

            var color = await SelectColor(getColorOptions);
            var modelYear = ReadModelYear();
            var manufactureYear = ReadManufactureYear();

            var command = new EditTruck(Truck.Chassis, color.Id, modelYear, manufactureYear);
            var result = await editTruck.ExecuteAsync(command);

            if (!result.IsSuccess) {
                WriteLine($"Não foi possível realizar a edição: {result.Message}.");
                return;
            }

            WriteLine("Caminhão editado com sucesso!");
        }

        private int ReadModelYear() {
            var validators = new List<IValidator> {
                new RequiredValidator("É necessário digitar o ano do modelo."),
                new ModelYearValidator("O ano deve ser igual ao atual."),
            };

            return Convert.ToInt32(ReadText("Digite o ano do modelo: ", validators));
        }

        private int ReadManufactureYear() {
            var validators = new List<IValidator> {
                new RequiredValidator("É necessário digitar o ano de fabricação."),
                new ManufactureYearValidator("O ano de fabricação deve ser igual ou maior que o atual."),
            };
            return Convert.ToInt32(ReadText("Digite ano da fabricação: ", validators));
        }
    }
}
