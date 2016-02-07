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
		}

		public DbSet<Issue> Issues { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Carton> Cartons { get; set; }
		public DbSet<LogAction> Logs { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Entity<ApplicationUser>()
				.HasMany(u => u.Assignments).WithRequired(i => i.AssignedTo);
			
			base.OnModelCreating(modelBuilder);
		}
	}
}