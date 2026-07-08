using Microsoft.EntityFrameworkCore;
using ProductionLineService.Models;

namespace ProductionLineService.Data
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{
		}

		// This property creates the link to your physical table
		public DbSet<ProductionLogDto> ProductionLogs { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			// Tells EF Core that LineId is the Primary Key
			modelBuilder.Entity<ProductionLogDto>().HasKey(p => p.LineId);
		}
	}
}