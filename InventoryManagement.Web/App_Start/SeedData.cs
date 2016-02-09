using System.Linq;
using InventoryManagement.Web.Data;
using InventoryManagement.Web.Domain;
using InventoryManagement.Web.Infrastructure.Tasks;

namespace InventoryManagement.Web
{
	public class SeedData : IRunAtStartup
	{
		private readonly ApplicationDbContext _context;

		public SeedData(ApplicationDbContext context)
		{
			_context = context;
		}

		public void Execute()
		{
            //var user1 = _context.Users.FirstOrDefault(u => u.UserName == "abdul") ??
            //            _context.Users.Add(new ApplicationUser {UserName = "abdul"});

			
            //_context.SaveChanges();

			
		}
	}
}