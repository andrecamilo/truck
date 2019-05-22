using System;
using System.Threading.Tasks;

namespace Truck.Infra.Helper
{
    /// <summary>
    /// Representa a interface para execução de um comando.
    /// </summary>
    public interface ICommand<TCommand>
    {
        Task<CommandResponse> ExecuteAsync(TCommand command);
    }

    /// <summary>
    /// Declara métodos de extensão utilitários para criação dos objetos de resposta de um comando.
    /// </summary>
    public static class ICommandExtensions
    {
        public static CommandResponse Success<TCommand>(this ICommand<TCommand> command) => new CommandResponse();

        public static CommandResponse Failure<TCommand>(this ICommand<TCommand> command, string message) => new CommandResponse(message, false);
    }
}
