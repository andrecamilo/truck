using System;
using System.Drawing;

namespace Truck.Domain.Register.Models
{
    /// <summary>
    /// Uma opção contendo o objeto base que ela representa, o nome para
    ///     exibição e uma cor opcional.
    /// </summary>
    public class Option<T>
    {
        /// <summary>
        /// Armazena uma referência para o objeto base que será selecionado.
        /// Genérico, para que qualquer tipo possa ser adaptado para esse modelo de interação.
        /// </summary>
        public T Object { get; }

        /// <summary>
        /// O nome que será exibido para essa opção.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// A cor que será usada para imprimir essa opção no terminal.
        /// Opcional.
        /// </summary>
        public Color? Color { get; }

        /// <summary>
        /// Construtor padrão recebe as propriedades já que a classe foi
        /// implementada como 'read-only'.
        /// </summary>
        public Option(T obj, string name, Color? color = null) {
            Object = obj;
            Name = name;
            Color = color;
        }
    }
}
