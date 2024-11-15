using System.ComponentModel.DataAnnotations;

namespace Bioinformatics.Models
{
	public class GeneticSample
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[StringLength(255)]
		public string FileName { get; set; }

		[StringLength(50)]
		public string DeterminedSex { get; set; }

		// Store depths as an array instead of individual properties
		public double[] Depths { get; set; }

		// Helper method to get display text for determined sex
		public string GetDeterminedSexDisplay()
		{
			if (string.IsNullOrEmpty(DeterminedSex))
				return "Unknown";
			return DeterminedSex;
		}
	}
}