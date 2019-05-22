using System;

namespace Truck.Domain.Register.Validation
{
    /// <summary>
    /// Abstração de um componente de validação.
    /// </summary>
    public interface IValidator
    {
        /// <summary>
        /// Aplica a validação.
        /// </summary>
        /// <returns>Uma tupla indicando se a validação foi bem sucedida e a mensagem de erro caso tenha falhado.</returns>
        /// <param name="value">O valor a ser validado.</param>
        (bool IsValid, string ErrorMessage) Validate(string value);
    }
}
