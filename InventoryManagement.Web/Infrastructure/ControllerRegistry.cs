﻿using StructureMap.Configuration.DSL;

namespace InventoryManagement.Web.Infrastructure
{
	public class ControllerRegistry : Registry
	{
		public ControllerRegistry()
		{
			Scan(scan =>
			{
				scan.TheCallingAssembly();
				scan.With(new ControllerConvention());
			});
		}
	}
}