using ProductionLineService.Models;

namespace ProductionLineService.Services
{
	public interface IProductionService
	{

		Task ProcessBatchAsync(IEnumerable<ProductionLogDto> LogBatch);
	}
}
