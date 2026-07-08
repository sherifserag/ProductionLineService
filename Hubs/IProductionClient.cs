using ProductionLineService.Models;

namespace ProductionLineService.Hubs
{
	public interface IProductionClient
	{
		Task OnLogBatchInserted(IEnumerable<ProductionLogDto> insertedBatch);
	}
}
