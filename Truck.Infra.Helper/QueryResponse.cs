using System;

namespace Truck.Infra.Helper
{
    /// <summary>
    /// Representa o resultado da execução de uma Query.
    /// </summary>
    public class QueryResponse<T>
    {
        public QueryResponse(T content) {
            IsSuccess = true;
            Content = content;
        }

        public QueryResponse(string message) {
            IsSuccess = false;
            Message = message;
        }

        public bool IsSuccess { get; set; }

        public string Message { get; set; }

        public T Content { get; set; }
    }
}
