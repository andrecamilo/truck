using System;
using System.Threading.Tasks;

namespace Truck.Domain.Register.Abstractions
{
    public interface IModule
    {
        Task ExecuteAsync();
    }
}
