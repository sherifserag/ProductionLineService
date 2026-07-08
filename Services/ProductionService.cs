using Microsoft.EntityFrameworkCore;
using ProductionLineService.Data;
using ProductionLineService.Models;
using ProductionLineService.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace ProductionLineService.Services
{
	public class ProductionService : IProductionService
	{
		private readonly AppDbContext _context;
		private readonly IHubContext<ProductionHub, IProductionClient> _hubContext;
		public ProductionService(AppDbContext context,IHubContext<ProductionHub, IProductionClient> hubContext)
		{
			_context = context;
			_hubContext = hubContext;
		}
		public async Task ProcessBatchAsync(IEnumerable<ProductionLogDto> logBatch)
		{
			using var transaction = await _context.Database.BeginTransactionAsync();
			try
			{
				// Add this line to manually confirm the service layer is receiving the array
				Console.WriteLine($"Service is not attempting to insert {logBatch.Count()} records.");

				await _context.ProductionLogs.AddRangeAsync(logBatch);
				await _context.SaveChangesAsync();

				await transaction.CommitAsync();

				await _hubContext.Clients.All.OnLogBatchInserted(logBatch);
			}
			catch (Exception ex)
			{
				await transaction.RollbackAsync();
				Console.WriteLine($"Database Write Failed: {ex.Message}");
				throw;
			}
		}
	}
}
