using System.Collections.Generic;
using System.Threading.Tasks;
using Truck.Domain.Register.Abstractions;
using Truck.Infra.Database;
using Truck.Infra.Database.Entities;
using Truck.Infra.Database.Models;
using Truck.Domain.Register.Models;
using Truck.Domain.Register.Services.Commands;
using Truck.Domain.Register.Services.Queries;
using Truck.Domain.Register.Validation;
using static System.Console;
using static Truck.Presentation.Console.ConsoleInteractions;
using Truck.Infra.Helper;
using System;

namespace Truck.Presentation.Console.Modules
{
    /// <summary>
    /// Módulo de inserção de Caminhãos.
    /// </summary>
    public class InsertTruckModule : IModule
    {
        readonly IGetColorOptionsQuery getColorOptions;
        readonly ICommand<InsertTruck> insertTruck;
        readonly TruckContext context;

        public InsertTruckModule(
                IGetColorOptionsQuery getColorOptions,
                ICommand<InsertTruck> insertTruck,
                TruckContext context) {
            this.getColorOptions = getColorOptions;
            this.insertTruck = insertTruck;
            this.context = context;
        }

        public async Task ExecuteAsync() {
            WriteLine();
            WriteLine("Insira os dados do novo Caminhão...");

            var chassis = ReadChassis();
            var color = await SelectColor(getColorOptions);
            var model = ReadModel();
            var modelYear = ReadModelYear();
            var manufactureYear = ReadManufactureYear();

            var Truck = new Infra.Database.Entities.Truck {
                Chassis = chassis,
                ModelYear = (int)modelYear,
                ManufactureYear = (int)manufactureYear,
                Model = model,
                ColorId = color.Id,
            };

            var command = new InsertTruck(Truck);
            var result = await insertTruck.ExecuteAsync(command);
            if (!result.IsSuccess) { 
                WriteLine($"Não foi possível criar o registro: {result.Message}");
                return;
            }

            WriteLine("Seu Caminhão foi criado com sucesso!");
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

        string ReadChassis() {
            var validators = new List<IValidator> {
                new RequiredValidator("É necessário digitar o chassi."),
                new ChassisExistsValidator(context, "Já há um Caminhão com esse chassi registrado."),
            };
            return ReadText("Digite o Chassi do Caminhão: ", validators);
        }

        TruckModel ReadModel() {
            var options = new List<Option<TruckModel>> {
                new Option<TruckModel>(TruckModel.FH, "FH"),
                new Option<TruckModel>(TruckModel.FM, "FM"),
            };
            return Select("Selecione o modelo do Caminhão", options);
        }
    }
}
