using F1ComparingWebApp.Models;
using F1ComparingWebApp.Models.Partials;
using F1ComparingWebApp.Service;
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

        public DriverComparisonController(IEregastAPI eregastAPI)
        {
            _eregastAPI = eregastAPI;
        }

        public async Task<IActionResult> IndexAsync()
        {
            var firstDriverQualifyingData = await _eregastAPI.GetQualifiyingOfCurrentSeasonByDriver("max_verstappen");
            var secondDriverQualifyingData = await _eregastAPI.GetQualifiyingOfCurrentSeasonByDriver("hamilton");

            var qualifyingDTO = await _eregastAPI.GetQualifyingOfCurrentSeasonAsync();

            var driverComparisonModel = new DriverComparisonModel()
            {
                Driver1Name = firstDriverQualifyingData.MrData.RaceTable.Races.FirstOrDefault().QualifyingResults.FirstOrDefault().Driver.GivenName,
                Driver2Name = secondDriverQualifyingData.MrData.RaceTable.Races.FirstOrDefault().QualifyingResults.FirstOrDefault().Driver.GivenName,
                Driver1GPdata = firstDriverQualifyingData.MrData.RaceTable.Races.Select(x => new DriverCompareGPData()
                {
                    Q1Time = x.QualifyingResults.FirstOrDefault().Q1,
                    Q2Time = x.QualifyingResults.FirstOrDefault().Q2,
                    Q3Time = x.QualifyingResults.FirstOrDefault().Q3,
                    GPName = x.RaceName
                }),
                Driver2GPdata = secondDriverQualifyingData.MrData.RaceTable.Races.Select(y => new DriverCompareGPData()
                {
                    Q1Time = y.QualifyingResults.FirstOrDefault().Q1,
                    Q2Time = y.QualifyingResults.FirstOrDefault().Q2,
                    Q3Time = y.QualifyingResults.FirstOrDefault().Q3,
                    GPName = y.RaceName
                })
            };


            return View(driverComparisonModel);
        }

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
