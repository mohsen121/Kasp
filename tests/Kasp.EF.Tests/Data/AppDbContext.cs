using Kasp.EF.Tests.Models.NewsModel;
using Microsoft.EntityFrameworkCore;

namespace Kasp.EF.Tests.Data {
	public class AppDbContext : KDbContext<AppDbContext> {
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {
		}

		public DbSet<News> Newses { get; set; }
	}
}