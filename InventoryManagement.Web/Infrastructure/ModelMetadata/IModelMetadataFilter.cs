using System;
using System.Collections.Generic;

namespace InventoryManagement.Web.Infrastructure.ModelMetadata
{
	public interface IModelMetadataFilter
	{
		void TransformMetadata(System.Web.Mvc.ModelMetadata metadata,
			IEnumerable<Attribute> attributes);
	}
}