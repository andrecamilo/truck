using System;
using System.Threading.Tasks;

namespace Truck.Infra.Helper
{
    /// <summary>
    /// Representa a interface para execução de uma query.
    /// </summary>
    public interface IQuery<TQuery, TResult>
    {
        Task<QueryResponse<TResult>> ExecuteAsync(TQuery query);
    }

    /// <summary>
    ///  Declara métodos de extensão utilitários para criação dos objetos de resposta de uma query.
    /// </summary>
    public static class IQueryExtensions
    {
        public static QueryResponse<TResult> Result<TQuery, TResult>(this IQuery<TQuery, TResult> query, TResult content) =>
            new QueryResponse<TResult>(content);

        public static QueryResponse<TResult> ResultError<TQuery, TResult>(this IQuery<TQuery, TResult> query, string msg) =>
            new QueryResponse<TResult>(msg);
    }
}
