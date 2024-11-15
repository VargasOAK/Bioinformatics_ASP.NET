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
				// Male sample with autosomal chromosomes around 25-35x, X ~13-18x, Y ~12-17x
				Depths = new double[] {
					34.1, 29.5, 27.8, 31.6, 26.9, 33.0,  // Chr 1-6
					28.3, 32.9, 30.7, 35.2, 29.9, 25.6,  // Chr 7-12
					33.5, 26.3, 30.1, 31.8, 29.0, 32.4,  // Chr 13-18
					34.7, 28.5, 31.2, 27.4,              // Chr 19-22
					16.2,                                 // Chr X (~half of autosomal)
					13.8                                  // Chr Y (~half of autosomal)
				}
},
new GeneticSample
{
				Id = 2,
				FileName = "Sample_002.bam",
				DeterminedSex = "Possible Female",
				// Female sample with autosomal chromosomes around 28-34x, X similar to autosomal, Y near zero
				Depths = new double[] {
					30.3, 32.1, 28.9, 33.4, 31.5, 29.7,  // Chr 1-6
					28.8, 33.2, 30.4, 32.8, 28.7, 29.5,  // Chr 7-12
					33.0, 29.2, 30.7, 28.6, 31.3, 30.2,  // Chr 13-18
					29.1, 33.4, 31.8, 32.2,              // Chr 19-22
					32.7,                                 // Chr X (similar to autosomal)
					0.1                                   // Chr Y (background noise)
				}
},
new GeneticSample
{
				Id = 3,
				FileName = "Sample_003.bam",
				DeterminedSex = "Possible Male",
				// Another male sample with autosomal chromosomes 25-35x, X ~13-18x, Y ~12-17x
				Depths = new double[] {
					27.9, 31.3, 34.2, 29.1, 30.8, 32.5,  // Chr 1-6
					28.5, 33.1, 29.6, 27.4, 30.2, 34.0,  // Chr 7-12
					29.7, 32.4, 27.1, 33.3, 30.4, 31.0,  // Chr 13-18
					30.8, 28.6, 32.9, 30.5,              // Chr 19-22
					15.7,                                 // Chr X (~half of autosomal)
					12.4                                  // Chr Y (~half of autosomal)
				}
},
new GeneticSample
{
				Id = 4,
				FileName = "Sample_004.bam",
				DeterminedSex = "Possible Female",
				// Another female sample with autosomal chromosomes 28-34x, X similar to autosomal, Y near zero
				Depths = new double[] {
					33.2, 30.4, 28.1, 32.3, 29.9, 30.8,  // Chr 1-6
					31.4, 33.0, 28.3, 31.9, 32.1, 28.7,  // Chr 7-12
					30.5, 29.8, 33.4, 28.5, 31.7, 30.0,  // Chr 13-18
					30.9, 29.6, 32.5, 33.1,              // Chr 19-22
					30.9,                                 // Chr X (similar to autosomal)
					0.3                                   // Chr Y (background noise)
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
