using Autofac;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Truck.Domain.Register.Abstractions;
using Truck.Domain.Register.Components.Commands;
using Truck.Domain.Register.Components.Queries;
using Truck.Domain.Register.Services.Commands;
using Truck.Domain.Register.Services.Queries;
using Truck.Infra.Database;
using Truck.Infra.Helper;
using Truck.Presentation.Console.Modules;
using static System.Console;            // Atalho para chamada de métodos estáticos do C# 7.0

namespace Truck.Presentation.Console
{
    class Program
    {
        /// <summary>
        /// Container de Resolução de Dependências.
        /// </summary>
        static IContainer Container;

        async static Task Main(string[] args)
        {
            PrintHeader();                  // Apresenta o cabeçalho de abertura do programa
            ApplicationStartup();           // Execute lógica de inicialização da aplicação

            // Main loop - Apresenta o menu principal e gerencia o acesso as opções
            var isContinue = true;
            do
            {
                PrintMenu();
                var input = ReadKey();
                var inputKey = (int)input.Key;
                WriteLine();

                if (IsValidOption(inputKey))
                {

                    var menuOption = (MenuOption)inputKey;
                    if (menuOption == MenuOption.Exit)
                        isContinue = false;
                    else
                        using (var scope = Container.BeginLifetimeScope())
                        {
                            var module = scope.ResolveKeyed<IModule>(menuOption);
                            await module.ExecuteAsync();
                        }
                }
                else
                {
                    WriteLine();
                    WriteLine("Opção inválida!");
                }
            } while (isContinue);
        }

        /// <summary>
        /// Lógica de Inicialização da Aplicação.
        /// </summary>
        static void ApplicationStartup()
        {
            Container = InitializeContainer();

            using (var scope = Container.BeginLifetimeScope()) {
                var context = scope.Resolve<TruckContext>();
                context.Database.EnsureCreated();
            }
        }

        /// <summary>
        /// Inicializa o Container de Resolução de Dependencia.
        /// </summary>
        /// <returns>O objeto IContainer construído.</returns>
        static IContainer InitializeContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<DeleteTruckCommand>().As<ICommand<DeleteTruck>>().InstancePerDependency();
            builder.RegisterType<EditTruckCommand>().As<ICommand<EditTruck>>().InstancePerDependency();
            builder.RegisterType<InsertTruckCommand>().As<ICommand<InsertTruck>>().InstancePerDependency();
            builder.RegisterType<FindTruckQuery>().As<IFindTruckQuery>().InstancePerDependency();
            builder.RegisterType<GetColorOptionsQuery>().As<IGetColorOptionsQuery>().InstancePerDependency();
            builder.RegisterType<GetTrucksQuery>().As<IGetTrucksQuery>().InstancePerDependency();

            // Registra os módulos usando a resolução por chave
            builder.RegisterType<InsertTruckModule>().Keyed<IModule>(MenuOption.Insert);
            builder.RegisterType<EditTruckModule>().Keyed<IModule>(MenuOption.Edit);
            builder.RegisterType<DeleteTruckModule>().Keyed<IModule>(MenuOption.Delete);
            builder.RegisterType<ListTrucksModule>().Keyed<IModule>(MenuOption.List);
            builder.RegisterType<FindTruckModule>().Keyed<IModule>(MenuOption.Find);

            // Registra a resolução do DbContext da aplicação
            // Isso permite determinamos na raiz da composição
            //  qual o banco de dados que será usado na persistência
            //  do context.
            var optionsBuilder = new DbContextOptionsBuilder<TruckContext>();
            var options = new DbContextOptionsBuilder<TruckContext>()
                .UseSqlite("Data Source=truck_db.db")
                .Options;

            //optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=truck.InMemory;Trusted_Connection=True;ConnectRetryCount=0");

            builder.RegisterType<TruckContext>()
                .WithParameter(new TypedParameter(typeof(DbContextOptions<TruckContext>), options));

            return builder.Build();
        }

        static void PrintHeader()
        {
            WriteLine("Truck Registration");
        }

        static void PrintMenu()
        {
            WriteLine();
            WriteLine("Escolha a opção desejada:");
            WriteLine("\t1. Inserir um Caminhão");
            WriteLine("\t2. Editar um Caminhão existente");
            WriteLine("\t3. Deletar um Caminhão existente");
            WriteLine("\t4. Listar todos os Caminhãos");
            WriteLine("\t5. Encontrar um Caminhão por chassi");
            WriteLine("\t0. Sair");
            WriteLine();
            Write("Digite o número da opção desejada: ");
        }

        /// <summary>
        /// Esse método verifica se o valor passado representa uma opção válida do menu.
        /// </summary>
        /// <returns>Verdadeiro se for uma opção valida.</returns>
        /// <param name="value">Valor numérico do parametro selecionado.</param>
        /// <remarks>
        /// Para facilitar a validação esse método verifica a existência do valor dentro
        ///     das opções da enumeração `MenuOption`, que representa o que o usuário pode
        ///     escolher.
        /// Usando o método Enum.GetValues que é não genérico, fazemos o cast para um Array
        ///     do tipo MenuOption e assim podemos aplicar o método Any() do LINQ para
        ///     verificar a existência do valor na lista.
        /// </remarks>
        static bool IsValidOption(int value) => 
        ((MenuOption[])Enum.GetValues(typeof(MenuOption))).Any(o => (int)o == value);

        /// <summary>
        /// Representa as opções do menu que usuário pode selecionar.
        /// Atribuimos a cada opção o código de tecla correspondente no Console.
        /// Isso facilita a verificação.
        /// </summary>
        enum MenuOption
        {
            Insert = ConsoleKey.D1,
            Edit = ConsoleKey.D2,
            Delete = ConsoleKey.D3,
            List = ConsoleKey.D4,
            Find = ConsoleKey.D5,
            Exit = ConsoleKey.D0,
        }
    }
}
