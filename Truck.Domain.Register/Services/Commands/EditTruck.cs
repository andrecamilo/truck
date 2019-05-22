using System;
namespace Truck.Domain.Register.Services.Commands
{
    public class EditTruck
    {
        public string Chassis { get; }

        public int ColorId { get; }
        public int ModelYear { get; }
        public int ManuYear { get; }

        public EditTruck(string chassis, int colorId, int modelYear, int manuYear) {
            Chassis = chassis;
            ColorId = colorId;
            ModelYear = modelYear;
            ManuYear = manuYear;
        }
    }
}
