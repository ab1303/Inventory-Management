using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace InventoryManagement.Web.Infrastructure.ModelMetadata.Filters
{
    public class CurrencyTextBoxConventionFilter : IModelMetadataFilter
	{
		public void TransformMetadata(System.Web.Mvc.ModelMetadata metadata,
			IEnumerable<Attribute> attributes)
		{
            if (!string.IsNullOrEmpty(metadata.PropertyName) &&
                string.IsNullOrEmpty(metadata.DataTypeName) &&
                metadata.PropertyName.ToLower().Contains("price"))
            {
                metadata.DataTypeName = "Currency";
            }
		}
	}
}