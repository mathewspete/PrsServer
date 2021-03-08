using Microsoft.EntityFrameworkCore;
using PrsServer.Models;

namespace PrsServer.Data {
	public class PrsServerContext : DbContext {
		public PrsServerContext(DbContextOptions<PrsServerContext> options)
				: base(options) {
		}

		public DbSet<User> Users { get; set; }

		protected override void OnModelCreating(ModelBuilder builder) {
			// create an index for the table?
			builder.Entity<User>(cust => {
				cust.HasIndex(x => x.Username).IsUnique(true); //config/get idx for Users to use for code column. each Unique
			});
		}

	}
}
