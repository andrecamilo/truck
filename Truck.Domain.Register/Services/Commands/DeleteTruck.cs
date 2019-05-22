using System;

namespace Truck.Domain.Register.Services.Commands
{
    public class DeleteTruck
    {
        public string Chassis { get; }

        public DeleteTruck(string chassis) => Chassis = chassis;
    }
}
