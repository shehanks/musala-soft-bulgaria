using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusalaSoft.Transpotation.Domain.Models
{
    public class BatteryData
    {
        public int DroneId { get; set; }

        public string DroneSerialNumber { get; set; } = string.Empty;

        public decimal Percentage { get; set; }
    }
}
