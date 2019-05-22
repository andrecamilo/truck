namespace Truck.Infra.Database.Entities
{
    /// <summary>
    /// Representa uma opção de cor que um Caminhão pode ter.
    /// </summary>
    public class ColorOption
    {
        /// <summary>
        /// Identificador da entidade. Optei por usar inteiro por ser uma entidade
        ///     simples de apoio. Num modelo mais elaborado talvez o código da cor
        ///     em hexadecimal pudesse ser usado como chave.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nome da Cor.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Representa a quantidade de vermelho na composição da cor na representação RGB.
        /// </summary>
        public byte Red { get; set; }

        /// <summary>
        /// Representa a quantidade de verde na composição da cor na representação RGB.
        /// </summary>
        public byte Green { get; set; }

        /// <summary>
        /// Representa a quantidade de azul na composição da cor na representação RGB.
        /// </summary>
        public byte Blue { get; set; }

        /// <summary>
        /// Retorna o código hexadecimal da representação RGB dessa cor.
        /// </summary>
        public string ToRgbHex() => $"#{Red:X2}{Green:X2}{Blue:X2}";
    }
}