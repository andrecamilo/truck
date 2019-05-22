using System;

namespace Truck.Domain.Register.Validation
{
    /// <summary>
    /// Valida se a entrada não é uma string vazia.
    /// </summary>
    public class ModelYearValidator : IValidator
    {
        private readonly string errorMessage;

        public ModelYearValidator(string errorMessage) => this.errorMessage = errorMessage;

        public (bool, string) Validate(string value) {
            int.TryParse(value, out var y);
            return (y == DateTime.Now.Year, errorMessage);
        }
    }
}
