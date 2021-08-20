using F1ComparingWebApp.Helpers;
using F1ComparingWebApp.Models;
using F1ComparingWebApp.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace F1ComparingWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IEregastAPI _eregastAPI;
        private CacheHelper _cacheHelper;

        public HomeController(ILogger<HomeController> logger, IEregastAPI eregastAPI, CacheHelper cache)
        {
            _logger = logger;
            _eregastAPI = eregastAPI;
            _cacheHelper = cache;
        }

        public IActionResult Index()
        {
            var allDriversInCurrentSeason = _cacheHelper.CachedResult("drivers-current-season", () =>
            {
                return AsyncHelper.RunSync(() => _eregastAPI.GetDriverOfCurrentSeason());
            }, new TimeSpan(12, 0, 0));

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
