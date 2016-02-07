using System.Security.Principal;
using InventoryManagement.Web.Data;
using InventoryManagement.Web.Domain;
using Microsoft.AspNet.Identity;

namespace InventoryManagement.Web.Infrastructure
{
	public class CurrentUser : ICurrentUser
	{
		private readonly IIdentity _identity;
		private readonly ApplicationDbContext _context;

		private ApplicationUser _user;

		public CurrentUser(IIdentity identity, ApplicationDbContext context)
		{
			_identity = identity;
			_context = context;
		}

		public ApplicationUser User
		{
			get { return _user ?? (_user = _context.Users.Find(_identity.GetUserId())); }
		}
	}
}