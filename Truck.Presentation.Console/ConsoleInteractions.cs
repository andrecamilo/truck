using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Truck.Domain.Register.Models;
using Truck.Domain.Register.Services.Queries;
using Truck.Domain.Register.Validation;
using Truck.Infra.Database.Entities;
using Truck.Presentation.Console.Extensions;
using static Colorful.Console;

namespace Truck.Presentation.Console
{
    /// <summary>
    /// Classe utilitária com métodos estáticos que componetizam diversos tipos
    ///     de interação do Console que a aplicação pode apresentar.
    /// </summary>
    public static class ConsoleInteractions
    {
        /// <summary>
        /// Representa uma interação no Console onde o usuário busca por um Caminhão
        ///     através do seu chasse.
        /// </summary>
        public static async Task<Infra.Database.Entities.Truck> FindTruck(IFindTruckQuery query) {
            do {
                Write("Digite o chassi do Caminhão: ");
                var chassis = ReadLine();
                if (string.IsNullOrWhiteSpace(chassis)) continue;

                var result = await query.ExecuteAsync(chassis);
                if (!result.IsSuccess) {
                    WriteLine(result.Message);
                    continue;
                }

                return result.Content;
            } while (true);
        }


        /// <summary>
        /// Imprime os dados de um Caminhão.
        /// </summary>
        /// <param name="Truck">Caminhão</param>
        public static void PrintTruck(Infra.Database.Entities.Truck Truck) {
            WriteLine($"- {Truck.ModelDesc}:");
            WriteLine($"\tChassi: {Truck.Chassis}");

            if (Truck.Color != null) {
                Write($"\tCor: ");
                WriteLine(Truck.Color.Name, Truck.Color.ToColor());
            }

            WriteLine($"\tAno do modelo: {Truck.ManufactureYear}");
            WriteLine($"\tAno de fabricação: {Truck.ModelYear}");
            WriteLine($"\tCapacidade: {Truck.Capacity}");
        }

        /// <summary>
        /// Apresenta uma lista de opções e retorna a seleção feita pelo usuário.
        /// Essa função facilita a criação de um seletor dentro do terminal.
        /// </summary>
        /// <returns>The select.</returns>
        /// <param name="prompt">O título que será apresentado no menu de seleção.</param>
        /// <param name="options">Uma lista com as opções que devem ser exibidas.</param>
        /// <typeparam name="T">O tipo do objeto que representa a opção a ser selecionada.</typeparam>
        public static T Select<T>(string prompt, IList<Option<T>> options) {
            do {
                WriteLine(prompt);
                for (var i = 0; i < options.Count; i++) {
                    var option = options[i];
                    var optionNumber = i + 1;

                    if (option.Color.HasValue)
                        WriteLine($"\t{optionNumber}. {option.Name}", option.Color.Value);
                    else
                        WriteLine($"\t{optionNumber}. {option.Name}");
                }

                Write("Digite o número da opção: ");
                var input = ReadLine();
                if (int.TryParse(input, out var inputNumber) &&
                    inputNumber > 0 && inputNumber <= options.Count) {
                    var index = inputNumber - 1;
                    return options[index].Object;
                }

                WriteLine("Opção inválida.");
            } while (true);
        }

        /// <summary>
        /// Apresenta a lista de cores para seleção pelo usuário.
        /// </summary>
        /// <returns>A cor selecionada.</returns>
        /// <param name="getColorOptions">Um objeto do tipo da Query de Cores.</param>
        public static async Task<ColorOption> SelectColor(IGetColorOptionsQuery getColorOptions) {
            var colors = await getColorOptions.ExecuteAsync(new GetColorOptions());
            var options = colors.Content
                .Select(c => new Option<ColorOption>(c, c.Name, c.ToColor()))
                .ToList();

            return Select("Selecione a cor desejada", options);
        }

        /// <summary>
        /// Apresenta uma interação para que o usuário digite um valor do tipo string.
        /// A interação irá aplicar os validadores passados como parâmetro garantido
        ///     que uma entrada valida será retornada para o chamador.
        /// </summary>
        /// <returns>The text.</returns>
        /// <param name="prompt">Texto apresentado no Console solicitando a digitação do valor.</param>
        /// <param name="validators">Uma enumeração com os va validadores que devem ser aplicados a entrada do usuário..</param>
        public static string ReadText(string prompt, IEnumerable<IValidator> validators) {
            do {
                Write(prompt);

                var input = ReadLine();
                if (!IsValid(input))
                    continue;

                return input;
            } while (true);

            // Função aninhada para isolar a lógica de validação
            bool IsValid(string input) {
                foreach (var validator in validators) {
                    var (isSuccess, errorMessage) = validator.Validate(input);
                    if (!isSuccess) {
                        WriteLine(errorMessage);
                        return false;
                    }
                }
                return true;
            }
        }
    }
}
