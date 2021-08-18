using System;

namespace F1ComparingWebApp.Models
{
    public class DriverCompareGPData
    {
        public string GPName { get; set; }

        public long Round { get; set; }

        public GPDriverResults GPDriverResults { get; set; }

        //qualifying info
        public string Q1Time { get; set; }
        public string Q2Time { get; set; }
        public string Q3Time { get; set; }

        public DateTime FastestTime { get; set; }
        public string FastestTimeDeficit { get; set; }

    }

    public class GPDriverResults
    {
        //race info
        public long GPStartingPos { get; set; }

        public long GPFinishedPos { get; set; }

        public long GPPoints { get; set; }

        public string GPTimeToLeader { get; set; }

        public bool GPFastestRaceLap { get; set; }

        public string GPFastestRaceLapTime { get; set; }

    }
}