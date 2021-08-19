using F1ComparingWebApp.Helpers;
using F1ComparingWebApp.Models;
using F1ComparingWebApp.Models.Partials;
using F1ComparingWebApp.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace F1ComparingWebApp.Controllers
{
    public class DriverComparisonController : Controller
    {
        private readonly IEregastAPI _eregastAPI;
        private CacheHelper _cacheHelper;

        public DriverComparisonController(IEregastAPI eregastAPI, CacheHelper cache)
        {
            _eregastAPI = eregastAPI;
            _cacheHelper = cache;
        }

        public IActionResult Index()
        {
            var firstDriverQualifyingData = _cacheHelper.CachedResult("quali-max_verstappen", () => 
            {
                return AsyncHelper.RunSync(() => _eregastAPI.GetQualifiyingOfCurrentSeasonByDriver("max_verstappen")); 
            }, new TimeSpan(0, 10, 0));
            var secondDriverQualifyingData = _cacheHelper.CachedResult("quali-alonso", () =>
            {
                return AsyncHelper.RunSync(() => _eregastAPI.GetQualifiyingOfCurrentSeasonByDriver("alonso"));
            }, new TimeSpan(0, 10, 0));
            var firstDriverRaceResultsData = _cacheHelper.CachedResult("races-max_verstappen", () =>
            {
                return AsyncHelper.RunSync(() => _eregastAPI.GetRaceResultsOfCurrentSeasonByDriver("max_verstappen"));
            }, new TimeSpan(0, 10, 0));
            var secondDriverRaceResultsData = _cacheHelper.CachedResult("races-alonso", () =>
            {
                return AsyncHelper.RunSync(() => _eregastAPI.GetRaceResultsOfCurrentSeasonByDriver("alonso"));
            }, new TimeSpan(0, 10, 0));

            var driverComparisonModel = new DriverComparisonModel()
            {
                Driver1Name = firstDriverQualifyingData.MrData.RaceTable.Races.FirstOrDefault().QualifyingResult.Driver.GivenName,
                Driver2Name = secondDriverQualifyingData.MrData.RaceTable.Races.FirstOrDefault().QualifyingResult.Driver.GivenName,
                Driver1GPdata = firstDriverQualifyingData.MrData.RaceTable.Races.Select(x => new DriverCompareGPData()
                {
                    Round = x.Round,
                    Q1Time = x.QualifyingResult.Q1,
                    Q2Time = x.QualifyingResult.Q2,
                    Q3Time = x.QualifyingResult.Q3,
                    GPName = x.RaceName,
                    FastestTime = GetFastestTime(x.QualifyingResult.Q1, x.QualifyingResult.Q2, x.QualifyingResult.Q3),
                    GPDriverResults = GetGPDriverResults(x.Round, firstDriverRaceResultsData)
                }),
                Driver2GPdata = secondDriverQualifyingData.MrData.RaceTable.Races.Select(y => new DriverCompareGPData()
                {
                    Round = y.Round,
                    Q1Time = y.QualifyingResult.Q1,
                    Q2Time = y.QualifyingResult.Q2,
                    Q3Time = y.QualifyingResult.Q3,
                    GPName = y.RaceName,
                    FastestTime = GetFastestTime(y.QualifyingResult.Q1, y.QualifyingResult.Q2, y.QualifyingResult.Q3),
                    GPDriverResults = GetGPDriverResults(y.Round, secondDriverRaceResultsData)
                })
            };

            foreach(var gp in driverComparisonModel.Driver1GPdata)
            {
                var test = GetFastestTimeDeficit(gp.FastestTime, driverComparisonModel.Driver2GPdata.FirstOrDefault(x => x.GPName == gp.GPName).FastestTime);
                gp.FastestTimeDeficit = test;
            }

            return View(driverComparisonModel);
        }

        private GPDriverResults GetGPDriverResults(long round, DriverRaceResultsData driverRaceResults)
        {
            var raceData = driverRaceResults.MrData.RaceTable.Races.FirstOrDefault(z => z.Round == round).Result;

            return new GPDriverResults()
            {
                GPStartingPos = raceData.Grid,
                GPFastestRaceLap = raceData?.FastestLap?.Rank == 1,
                GPPoints = raceData.Points,
                GPFinishedPos = raceData.Position,
                GPTimeToLeader = raceData.Time != null ? raceData?.Time?.Time : "",
                GPFastestRaceLapTime = raceData?.FastestLap?.Time.Time,
            };
        }


        //public string GPStartingPos { get; set; }

        //public string GPFinishedPos { get; set; }

        //public string GPPoints { get; set; }

        //public string GPTimeToLeader { get; set; }

        //public bool GPFastestRaceLap { get; set; }

        private static DateTime GetFastestTime(string q1Time, string q2Time, string q3Time)
        {
            DateTime.TryParseExact(q1Time, "m:ss.fff", null, System.Globalization.DateTimeStyles.None, out var q1DateTime);
            DateTime.TryParseExact(q2Time, "m:ss.fff", null, System.Globalization.DateTimeStyles.None, out var q2DateTime);
            DateTime.TryParseExact(q3Time, "m:ss.fff", null, System.Globalization.DateTimeStyles.None, out var q3DateTime);

            //filter times that are 00:00:00, this will happen when a timing is empty
            DateTime fastestTime = new[] { q1DateTime, q2DateTime, q3DateTime }.Where(x => x != new DateTime()).Min();
            return fastestTime;
        }

        private static string GetFastestTimeDeficit(DateTime driver1FastestTime, DateTime driver2FastestTime)
        {
            var timeDeficit = driver1FastestTime - driver2FastestTime;

            return timeDeficit.TotalSeconds.ToString();
        }

        //private DateTime GetFastestTime(DateTime time1, DateTime time2, DateTime time3)
        //{
        //    var fastestTime = time1;
        //    if (time1 > time2)
        //    {
        //        fastestTime = time2;
        //    }
        //    if (time2 > time3)
        //    {
        //        fastestTime = time3;
        //    }
        //    if (time1 > time3)
        //    {
        //        fastestTime = time3;
        //    }
        //    return fastestTime;
        //}

        //private DriverComparisonModel GetDriverCompareDto(QualifyingData data)
        //{
        //    var numbers = ["33", "44"];

        //    //return new DriverComparisonModel()
        //    //{
        //    //    Driver1Name = data.MrData.RaceTable.Races.Where(x => x.QualifyingResults.Where(y => y.Driver.PermanentNumber == numbers[0])),
        //    //    Driver2Name = data.MrData.RaceTable.Races.Where(x => x.QualifyingResults.Where(y => y.Driver.PermanentNumber == numbers[1])),
        //    //    Driver1GPdata = new DriverCompareGPData()
        //    //    {
        //    //        Q1Time = 
        //    //    }
        //    //}
        //}

        [HttpPost]
        public IActionResult Drivers()
        {
            return View();
        }
    }
}
