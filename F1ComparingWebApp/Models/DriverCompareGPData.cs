using System;

namespace F1ComparingWebApp.Models
{
    public class DriverCompareGPData
    {
        public string GPName { get; set; }
        public string Q1Time { get; set; }
        public string Q2Time { get; set; }
        public string Q3Time { get; set; }

        public DateTime FastestTime { get; set; }
        public string FastestTimeDeficit { get; set; }

    }
}