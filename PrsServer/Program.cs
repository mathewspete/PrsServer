using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrsServer {
	public class Program {
		public static void Main(string[] args) {
			CreateHostBuilder(args).Build().Run();
		}

		public static IHostBuilder CreateHostBuilder(string[] args) =>
				Host.CreateDefaultBuilder(args)
						.ConfigureWebHostDefaults(webBuilder => {
							webBuilder.UseStartup<Startup>();
						});
	}
}



#region SETUP
/* EntityFrameworkCore.Tools and EFC.SQLServer
 * Right click PrsServer > Prop > Debug Delete weather
 * appsettings.json
 *	|	"AllowedHost":"*",
 *	|	"ConnectionStrings":{
 *	|		"PrsDb": "server=localhost\\sqlexpress;database=PrsDb;trusted_connection=true;"
 *	
 * 
 * ---- Add Classes
 * 
 * ----- Add Controllers
 * Controller See pics... 2021/03/03
 * [if error: tools > manage nuget package > design should have like 3.1.4 
 
 * 
 * 
 * 
 * #### PrsServerContext
 * 
 * public DbSet<[DELETE]User> Users { get; set; } //F2 on "User" Change to "Users"
 * 
 * protected override void OnModelCreating(ModelBuilder builder) {
			// create an index for the table?
			builder.Entity<User>(cust => {
				cust.HasIndex(x => x.Username).IsUnique(true); //config/get idx for Users to use for code column. each Unique
			});
		}//// see EFCodeFirstTut for example 
 * 
 * 
 * ### Startop.cs
 * 
 * services.AddDbContext<PrsServerContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("PrsServerContext")));
		} -------------
 * ---------------------
 * --------->>>
 * services.AddDbContext<PrsServerContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("PrsDb"))); // needs to match Context connection string (data folder)

			services.AddCors();
		}
 * --------Add------
 * app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
 * 
 * ### appsettings.json
 * -----Delete----
 * "PrsServerContext": "Server=(localdb)\\mssqllocaldb;Database=PrsServerContext-2b2d3d3f-35b1-411d-b044-0aaa40a412ce;Trusted_Connection=True;MultipleActiveResultSets=true"
 * 
 * 
 * 
 * PM> add-migration "comment"
 * PM> update-database
 * 
 *
 * 
 */
#endregion