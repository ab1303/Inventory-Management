using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using InventoryManagement.Web.Filters;
using Microsoft.Web.Mvc;

namespace InventoryManagement.Web.Infrastructure
{
	[UserSelectListPopulator]
	public abstract class InventoryManagementController : Controller
	{
		protected ActionResult RedirectToAction<TController>(Expression<Action<TController>> action)
			where TController : Controller
		{
			return ControllerExtensions.RedirectToAction(this, action);
		}
	}
}