using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Курсовой_проект.Models
{
    public class CharacteristicModel
    {
        public int CharacteristicId { get; set; }
        public string Power { get; set; }
        public string Gearbox { get; set; }
        public double Acceleration { get; set; }
        public int MaxSpeed { get; set; }
        public int PowerReserve { get; set; }
        public string DriveUnit { get; set; }

        public CharacteristicModel(int characteristicId, string power, string gearbox, double acceleration, int maxSpeed, int powerReserve, string driveUnit) {
            CharacteristicId = characteristicId;
            Power = power;
            Gearbox = gearbox;
            Acceleration = acceleration;
            MaxSpeed = maxSpeed;
            PowerReserve = powerReserve;
            DriveUnit = driveUnit;
        }
    }
}
