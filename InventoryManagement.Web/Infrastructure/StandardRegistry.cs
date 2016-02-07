using StructureMap.Configuration.DSL;

namespace InventoryManagement.Web.Infrastructure
{
	public class StandardRegistry : Registry
	{
		public StandardRegistry()
		{
			Scan(scan =>
			{
				scan.TheCallingAssembly();
				scan.WithDefaultConventions();
			});
		}
	}
}