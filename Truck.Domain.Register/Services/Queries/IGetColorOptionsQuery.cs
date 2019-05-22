using System;
using System.Collections.Generic;
using Truck.Infra.Helper;
using Truck.Infra.Database.Entities;

namespace Truck.Domain.Register.Services.Queries
{
    public interface IGetColorOptionsQuery : IQuery<GetColorOptions, IEnumerable<ColorOption>>
    {
    }

    public class GetColorOptions
    {
    }
}
