using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductionLineService.Models;
using ProductionLineService.Services;

namespace ProductionLineService.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class ProductionController : ControllerBase
	{

		private readonly IProductionService _productionService;
		private readonly ILogger<ProductionController> _logger;

		public ProductionController(IProductionService productionService, ILogger<ProductionController> logger)
		{
			_productionService = productionService;
			_logger = logger;
		}

		[HttpPost("process-batch")]
		public async Task<IActionResult> ProcessBatch([FromBody] IEnumerable<ProductionLogDto> LogBatch)
		{
			if (LogBatch == null || !LogBatch.Any())
			{
				return BadRequest(new { message = "Batch payload cannot be empty." });
			}
			try
			{
				await _productionService.ProcessBatchAsync(LogBatch);
				return Ok(new { message = "Batch processed successfully." });
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error starting production");
				return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Error starting production" });
			}
		}
	}
}
