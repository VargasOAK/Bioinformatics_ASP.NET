using Microsoft.AspNetCore.Mvc;

namespace Bioinformatics.Controllers
{
    public class Dashboards : Controller
    {
        public IActionResult GeneralReport()
        {
            return View();
        }

        public IActionResult SamplesDetails()
        {
            return View();
        }

        public IActionResult GenderDiscovery()
        {
            return View();
        }
    }
}
