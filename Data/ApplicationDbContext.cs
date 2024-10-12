using Microsoft.EntityFrameworkCore;
using learndotnet.Models;

namespace learndotnet.Data
{
	// db context will manage all enitiies 
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
		
		public DbSet<GameModel> GameTable { get; set; }	
	}
}