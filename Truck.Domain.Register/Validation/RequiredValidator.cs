using System;

namespace Truck.Domain.Register.Validation
{
    /// <summary>
    /// Valida se a entrada não é uma string vazia.
    /// </summary>
    public class RequiredValidator : IValidator
    {
        private readonly string errorMessage;

        public RequiredValidator(string errorMessage) => this.errorMessage = errorMessage;

        public (bool, string) Validate(string value) => (!string.IsNullOrWhiteSpace(value), errorMessage);
    }
}
