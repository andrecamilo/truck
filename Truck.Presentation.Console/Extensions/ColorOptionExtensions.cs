using System;
using System.Drawing;
using Truck.Infra.Database.Entities;

namespace Truck.Presentation.Console.Extensions
{
    public static class ColorOptionExtensions
    {
        /// <summary>
        /// Retorna a representa dessa cor como um objeto System.Drawing.Color.
        /// </summary>
        public static Color ToColor(this ColorOption c) => Color.FromArgb(c.Red, c.Green, c.Blue);
    }
}
