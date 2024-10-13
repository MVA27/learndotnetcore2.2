using Microsoft.EntityFrameworkCore;
using learndotnet.Models;

namespace learndotnet.Data
{
	// db context will manage all enitiies 
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
		
		public DbSet<GameModel> GameTable { get; set; }	

		//Initialize DB with data during migration
		protected override void OnModelCreating(ModelBuilder modelBuilder){
			modelBuilder.Entity<GameModel>().HasData(
				new GameModel { Id=1, Name="PUBG", Device = "mobile", HashKey = "H#adwe@nd"}
			);
		}
	}
}