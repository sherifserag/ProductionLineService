namespace ProductionLineService.Models
{
	public class ProductionLogDto
	{
		public int LineId { get; set; }
		public DateTime Timestamp { get; set; }

		public string Status { get; set; } = string.Empty;

		public double Throughput { get; set; }

		public string Message { get; set; } = string.Empty;
		
		public string Operator { get; set; } = string.Empty;
	}
}
