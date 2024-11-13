using Microsoft.AspNetCore.Mvc;

namespace Bioinformatics.Controllers
{
	public class Samples : Controller
	{
		public IActionResult MinionReport()
		{
			string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "minion", "report.html");
			string htmlContent = System.IO.File.ReadAllText(path);

			return Content(htmlContent, "text/html");
		}
	}
}
