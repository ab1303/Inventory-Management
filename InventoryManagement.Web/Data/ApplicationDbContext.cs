using System.Data.Entity;
using InventoryManagement.Web.Domain;
using Microsoft.AspNet.Identity.EntityFramework;

namespace InventoryManagement.Web.Data
{
	public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
	{
		public ApplicationDbContext()
			: base("DefaultConnection")
		{
            //Create database always, even If exists
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<ApplicationDbContext>());
		}

        public DbSet<Item> Items { get; set; }
        public DbSet<Carton> Cartons { get; set; }
        public DbSet<Inventory> Inventories { get; set; }
		public DbSet<LogAction> Logs { get; set; }
	}
}