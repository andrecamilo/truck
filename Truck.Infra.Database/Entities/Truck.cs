using System.Collections.Generic;
using Truck.Infra.Database.Models;

namespace Truck.Infra.Database.Entities
{
    public class Truck
    {
        /// <summary>
        /// Chassi do Caminhão. É também a chave da entidade.
        /// </summary>
        public string Chassis { get; set; }

        /// <summary>
        /// Registra o modelo de Caminhão como uma enumeração. No banco de dados será
        ///     persistido como um inteiro, tipo base da enumeração.
        /// </summary>
        public TruckModel Model { get; set; }

        /// <summary>
        /// Obtém a descrição do tipo de Caminhão.
        /// </summary>
        public string ModelDesc {
            get {
                switch (Model) {
                    case TruckModel.FM: return "FM";
                    case TruckModel.FH: return "FH";
                    default: return null;
                }
            }
        }

        /// <summary>
        /// Capacidade de passageiros do Caminhão.
        /// Obtém a partir do dictionary estático que registra um de-para em memória,
        ///     simplificando a resolução da informação.
        /// </summary>
        public byte Capacity => truckModelCapacity[Model];

        public int ManufactureYear { get; set; }
        public int ModelYear { get; set; }

        /// <summary>
        /// Identificador da cor do Caminhão.
        /// </summary>
        public int ColorId { get; set; }

        /// <summary>
        /// Cor do Caminhão.
        /// </summary>
        public ColorOption Color { get; set; }

        /// <summary>
        /// Armazena em memória uma estrutura para obter rapidamente a capacidade de passageiros
        ///     de acordo com o tipo de Caminhão. Considero uma solução mais elegante, legível e
        ///     simples de implementar do que uma estrutura de `switch`.
        /// </summary>
        static readonly Dictionary<TruckModel, byte> truckModelCapacity = new Dictionary<TruckModel, byte> {
            [TruckModel.FH] = 2,
            [TruckModel.FM] = 42,
        };
    }
}
