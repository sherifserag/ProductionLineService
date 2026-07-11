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
		public DbSet<User> Users { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			// Tells EF Core that LineId is the Primary Key
			modelBuilder.Entity<ProductionLogDto>().HasKey(p => p.LineId);
			modelBuilder.Entity<User>().HasKey(u => u.Id);
			modelBuilder.Entity<User>().Property(u => u.Username).IsRequired();
			modelBuilder.Entity<User>().Property(u => u.Password).IsRequired();


		}
	}
}