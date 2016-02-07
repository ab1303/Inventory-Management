using InventoryManagement.Web.Domain;

namespace InventoryManagement.Web.Infrastructure
{
	public interface ICurrentUser
	{
		ApplicationUser User { get; } 
	}
}