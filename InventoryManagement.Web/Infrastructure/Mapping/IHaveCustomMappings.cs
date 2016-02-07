using AutoMapper;

namespace InventoryManagement.Web.Infrastructure.Mapping
{
	public interface IHaveCustomMappings
	{
		void CreateMappings(IConfiguration configuration);
	}
}