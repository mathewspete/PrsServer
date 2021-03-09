using Microsoft.EntityFrameworkCore;
using PrsServer.Models;

namespace PrsServer.Data {
	public class PrsDbContext : DbContext {
		public PrsDbContext(DbContextOptions<PrsDbContext> options)
				: base(options) {
		}

		public DbSet<User> Users { get; set; }

		public DbSet<Product> Product { get; set; }

		public DbSet<Request> Request { get; set; }

		public DbSet<RequestLine> RequestLine { get; set; }

		public DbSet<Vendor> Vendor { get; set; }

		protected override void OnModelCreating(ModelBuilder builder) {
			// create an index for the table?
			builder.Entity<User>(usr => {
				usr.HasIndex(x => x.Username).IsUnique(true); //config/get idx for Users to use for code column. each Unique
			});

			builder.Entity<Vendor>(vdr => {
				vdr.HasIndex(x => x.Code).IsUnique(true); //config/get idx for Users to use for code column. each Unique
			});

			builder.Entity<Product>(prod => {
				prod.HasIndex(x => x.PartNbr).IsUnique(true); //config/get idx for Users to use for code column. each Unique
			});

			builder.Entity<RequestLine>().Property(p => p.Quantity).HasDefaultValue(1);
			builder.Entity<Request>().Property(p => p.DeliveryMode).HasDefaultValue("Pickup");
			builder.Entity<Request>().Property(p => p.Status).HasDefaultValue("NEW");
			builder.Entity<Request>().Property(p => p.Total).HasDefaultValue(0);

		}

	}
}
