using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InventoryManagement.Web.Data;
using InventoryManagement.Web.Infrastructure;
using StructureMap;

namespace InventoryManagement.Web.Controllers
{
	public class HomeController : InventoryManagementController
	{
		public HomeController()
		{
		}

		public ActionResult Index()
		{
			return View();
		}
	}
}