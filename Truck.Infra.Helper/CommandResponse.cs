using System;

namespace Truck.Infra.Helper
{
    /// <summary>
    /// Objeto contendo o Status da execução de um comando.
    /// </summary>
    public class CommandResponse
    {
        public CommandResponse(bool isSuccess = true) => IsSuccess = isSuccess;

        public CommandResponse(string message, bool isSuccess = false) {
            Message = message;
            IsSuccess = isSuccess;
        }

        public bool IsSuccess { get; private set; }

        public string Message { get; private set; }
    }
}
