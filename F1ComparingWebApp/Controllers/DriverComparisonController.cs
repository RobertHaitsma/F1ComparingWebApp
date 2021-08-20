using F1ComparingWebApp.Helpers;
using F1ComparingWebApp.Models;
using F1ComparingWebApp.Models.Partials;
using F1ComparingWebApp.Models.Partials.F1;
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

        [HttpGet]
        public IActionResult Index()
        {
            //by default use max and lewis - future change this to appSettings key
            var driverComparisonModel = GetDriverComparisonModel("max_verstappen", "hamilton");

            driverComparisonModel.Driver1GPdata.Select(gp => 
            { 
                gp.FastestTimeDeficit = GetFastestTimeDeficit(gp.FastestTime, driverComparisonModel.Driver2GPdata.FirstOrDefault(x => x.GPName == gp.GPName).FastestTime); 
                return gp; 
            }).ToList();

            return View(driverComparisonModel);
        }

        private string GetDriversName(Driver driver) 
        {
            return driver.GivenName + " " + driver.FamilyName;
        }

        private IEnumerable<Tuple<string, string>> SetDriverNamesAndId(DriversInSeasonData driversInSeason)
        {
            return driversInSeason.MrData.DriverTable.Drivers.Select(x =>
            {
                return new Tuple<string, string>(x.DriverId, x.GivenName + " " + x.FamilyName);
            });
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

        //made a different implementation of this method. When in the future a q4 will be added, by using this function only one change has to be made, where the old function this would be 3 places
        private static DateTime GetFastestTime(IEnumerable<string> times)
        {
            return times.Select(x =>
            {
                DateTime.TryParseExact(x, "m:ss.fff", null, System.Globalization.DateTimeStyles.None, out var dateTime);
                return dateTime;
            }).Where(y => y != new DateTime()).Min();
        }

        private static string GetFastestTimeDeficit(DateTime driver1FastestTime, DateTime driver2FastestTime)
        {
            var timeDeficit = driver1FastestTime - driver2FastestTime;

            return timeDeficit.TotalSeconds.ToString();
        }

        private DriverComparisonModel GetDriverComparisonModel(string firstDriverId, string secondDriverId)
        {
            var allDriversInCurrentSeason = _cacheHelper.CachedResult("drivers-current-season", () =>
            {
                return AsyncHelper.RunSync(() => _eregastAPI.GetDriverOfCurrentSeason());
            }, new TimeSpan(12, 0, 0));

            var firstDriverQualifyingData = _cacheHelper.CachedResult($"quali-{firstDriverId}", () =>
            {
                return AsyncHelper.RunSync(() => _eregastAPI.GetQualifiyingOfCurrentSeasonByDriver(firstDriverId));
            }, new TimeSpan(0, 10, 0));
            var secondDriverQualifyingData = _cacheHelper.CachedResult($"quali-{secondDriverId}", () =>
            {
                return AsyncHelper.RunSync(() => _eregastAPI.GetQualifiyingOfCurrentSeasonByDriver(secondDriverId));
            }, new TimeSpan(0, 10, 0));
            var firstDriverRaceResultsData = _cacheHelper.CachedResult($"races-{firstDriverId}", () =>
            {
                return AsyncHelper.RunSync(() => _eregastAPI.GetRaceResultsOfCurrentSeasonByDriver(firstDriverId));
            }, new TimeSpan(0, 10, 0));
            var secondDriverRaceResultsData = _cacheHelper.CachedResult($"races-{secondDriverId}", () =>
            {
                return AsyncHelper.RunSync(() => _eregastAPI.GetRaceResultsOfCurrentSeasonByDriver(secondDriverId));
            }, new TimeSpan(0, 10, 0));

            var driverComparisonModel = new DriverComparisonModel()
            {
                DriverList = SetDriverNamesAndId(allDriversInCurrentSeason),
                Driver1Name = GetDriversName(firstDriverQualifyingData.MrData.RaceTable.Races.FirstOrDefault().QualifyingResult.Driver),
                Driver2Name = GetDriversName(secondDriverQualifyingData.MrData.RaceTable.Races.FirstOrDefault().QualifyingResult.Driver),
                Driver1GPdata = firstDriverQualifyingData.MrData.RaceTable.Races.Select(x => new DriverCompareGPData()
                {
                    Round = x.Round,
                    Q1Time = x.QualifyingResult.Q1,
                    Q2Time = x.QualifyingResult.Q2,
                    Q3Time = x.QualifyingResult.Q3,
                    GPName = x.RaceName,
                    FastestTime = GetFastestTime(new List<string>() { x.QualifyingResult.Q1, x.QualifyingResult.Q2, x.QualifyingResult.Q3 }),
                    GPDriverResults = GetGPDriverResults(x.Round, firstDriverRaceResultsData)
                }),
                Driver2GPdata = secondDriverQualifyingData.MrData.RaceTable.Races.Select(y => new DriverCompareGPData()
                {
                    Round = y.Round,
                    Q1Time = y.QualifyingResult.Q1,
                    Q2Time = y.QualifyingResult.Q2,
                    Q3Time = y.QualifyingResult.Q3,
                    GPName = y.RaceName,
                    FastestTime = GetFastestTime(new List<string>() { y.QualifyingResult.Q1, y.QualifyingResult.Q2, y.QualifyingResult.Q3 }),
                    GPDriverResults = GetGPDriverResults(y.Round, secondDriverRaceResultsData)
                })
            };

            return driverComparisonModel;
        }

        [HttpPost]
        public IActionResult Index(SelectedDriversModel model)
        {
            var driverComparisonModel = GetDriverComparisonModel(model.FirstDriverId, model.SecondDriverId);

            return View(driverComparisonModel);
        }

        [HttpPost]
        public IActionResult Drivers()
        {
            return View();
        }
    }
}
