using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper.QueryableExtensions;
using InventoryManagement.Web.Data;
using InventoryManagement.Web.Filters;
using InventoryManagement.Web.Infrastructure;
using InventoryManagement.Web.Infrastructure.Alerts;
using InventoryManagement.Web.Models.Carton;
using InventoryManagement.Web.Domain;
using InventoryManagement.Web.Helpers;
using System.Globalization;

namespace InventoryManagement.Web.Controllers
{
    public class CartonController : InventoryManagementController
    {
        private readonly ApplicationDbContext _context;

        public CartonController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Carton
        public ActionResult Index()
        {
            var models = _context.Cartons
                  .Project().To<CartonSummaryViewModel>();
            return View(models.ToArray());
        }

        public ActionResult New()
        {
            var form = new NewCartonForm
            {
                ItemList =
                    _context.Items.ToListItems(t => (t.ItemCode + " - " + t.ItemName),
                        v => v.ItemId.ToString(CultureInfo.InvariantCulture), "Please Select...")
            };


            return View(form);
        }

        [HttpPost, ValidateAntiForgeryToken, Log("Created Carton")]
        public ActionResult New(NewCartonForm form)
        {
            if (!ModelState.IsValid)
            {
                return View(form);
            }

            _context.Cartons.Add(new Carton
            {
                ItemId = form.ItemId,
                NumberOfPieces = form.NumberOfPieces
                
            });

            _context.SaveChanges();

            return RedirectToAction<CartonController>(c => c.Index())
                .WithSuccess("Carton created!");
        }

    }
}