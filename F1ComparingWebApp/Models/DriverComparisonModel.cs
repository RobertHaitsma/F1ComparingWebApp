using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace F1ComparingWebApp.Models
{
    public class DriverComparisonModel
    {
        public string Driver1Name { get; set; }

        public string Driver2Name { get; set; }

        public IEnumerable<DriverCompareGPData> Driver1GPdata { get; set; }

        public IEnumerable<DriverCompareGPData> Driver2GPdata { get; set; }

    }
}
