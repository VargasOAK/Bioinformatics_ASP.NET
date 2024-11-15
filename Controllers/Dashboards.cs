using Microsoft.AspNetCore.Mvc;
using Bioinformatics.Models;
using System.Collections.Generic;

namespace Bioinformatics.Controllers
{
	public class Dashboards : Controller
	{
		private List<GeneticSample> GetSampleData()
		{
			return new List<GeneticSample>
			{
				new GeneticSample
				{
					Id = 1,
					FileName = "Sample_001.bam",
					DeterminedSex = "Possible Male",
					// Depths array represents coverage for chromosomes 1-22, X, Y
					// Males typically show:
					// - Consistent autosomal (chr 1-22) coverage ~30x
					// - X chromosome coverage ~half of autosomal (since only one copy)
					// - Y chromosome coverage ~half of autosomal (since only one copy)
					Depths = new double[] {
						31.2, 30.8, 30.5, 31.1, 29.9, 30.4,  // Chr 1-6
						30.7, 31.3, 30.2, 30.9, 30.1, 30.6,  // Chr 7-12
						31.0, 30.4, 30.8, 29.8, 30.5, 31.2,  // Chr 13-18
						30.3, 30.9, 30.2, 30.7,              // Chr 19-22
						15.8,                                 // Chr X (~half coverage)
						14.9                                  // Chr Y (~half coverage)
					}
				},
				new GeneticSample
				{
					Id = 2,
					FileName = "Sample_002.bam",
					DeterminedSex = "Possible Female",
					// Females typically show:
					// - Consistent autosomal (chr 1-22) coverage ~30x
					// - X chromosome coverage similar to autosomal (since two copies)
					// - Y chromosome coverage near zero (since no Y chromosome)
					Depths = new double[] {
						30.8, 31.2, 30.4, 30.9, 30.1, 30.7,  // Chr 1-6
						31.1, 30.5, 30.8, 30.2, 30.9, 30.3,  // Chr 7-12
						30.6, 31.0, 30.4, 30.8, 30.2, 30.9,  // Chr 13-18
						30.5, 30.8, 31.1, 30.4,              // Chr 19-22
						30.6,                                 // Chr X (normal coverage)
						0.3                                   // Chr Y (background noise)
					}
				},
				new GeneticSample
				{
					Id = 3,
					FileName = "Sample_003.bam",
					DeterminedSex = "Possible Male",
					// Another male sample with slightly different coverage pattern
					// but maintaining the expected ratios
					Depths = new double[] {
						29.8, 30.4, 30.9, 30.2, 31.1, 30.5,  // Chr 1-6
						30.8, 30.1, 30.7, 31.2, 30.3, 30.9,  // Chr 7-12
						30.2, 30.8, 31.0, 30.4, 30.9, 30.1,  // Chr 13-18
						30.7, 30.2, 30.8, 30.5,              // Chr 19-22
						15.2,                                 // Chr X
						14.7                                  // Chr Y
					}
				},
				new GeneticSample
				{
					Id = 4,
					FileName = "Sample_004.bam",
					DeterminedSex = "Possible Female",
					// Another female sample showing typical pattern
					Depths = new double[] {
						30.5, 30.9, 30.2, 30.8, 31.1, 30.4,  // Chr 1-6
						30.7, 31.2, 30.3, 30.9, 30.1, 30.8,  // Chr 7-12
						31.0, 30.4, 30.7, 30.2, 30.9, 30.5,  // Chr 13-18
						30.8, 31.1, 30.3, 30.6,              // Chr 19-22
						31.0,                                 // Chr X
						0.2                                   // Chr Y
					}
				}
			};
		}

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
			var geneticData = GetSampleData();
			return View(geneticData);
		}

		[HttpGet]
		public IActionResult GetGeneticData()
		{
			var geneticData = GetSampleData();
			return Json(geneticData);
		}
	}
}
