using System;
using Truck.Infra.Database.Entities;

namespace Truck.Domain.Register.Services.Commands
{
    /// <summary>
    /// Comando para inserção de um Caminhão.
    /// </summary>
    public class InsertTruck
    {
        public Truck.Infra.Database.Entities.Truck Truck { get; }

        public InsertTruck(Truck.Infra.Database.Entities.Truck truck) => Truck = truck;
    }
}
