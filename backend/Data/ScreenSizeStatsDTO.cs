using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharpCounter.Data
{
    public class ScreenSizeStatsDTO
    {
        public int NumberOfPhones { get; set; }
        public int LargePhonesSmallTablets { get; set; }
        public int TabletsSmallLaptops { get; set; }
        public int ComputerMonitors { get; set; }
        public int ComputerMonitors4K { get; set; }
    }
}
