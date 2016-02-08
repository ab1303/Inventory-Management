using System;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using InventoryManagement.Web.Data;
using InventoryManagement.Web.Domain;
using InventoryManagement.Web.Filters;
using InventoryManagement.Web.Helpers;
using InventoryManagement.Web.Infrastructure;
using InventoryManagement.Web.Infrastructure.Alerts;
using InventoryManagement.Web.Models.Inventory;

namespace InventoryManagement.Web.Controllers
{
    public class InventoryController : InventoryManagementController
    {

        private readonly ApplicationDbContext _context;
        public InventoryController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: Inventory
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddPiece()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken, Log("Add Piece")]
        public ActionResult AddPiece(ItemInventory formItemInventory)
        {
            return View();
        }

        public ActionResult SellPiece()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken, Log("Sell Piece")]
        public ActionResult SellPiece(ItemInventory formItemInventory)
        {
            return View();
        }


        # region "Carton Inventory"

        public ActionResult AddCarton()
        {
            var form = new CartonInventory
            {
                CartonList =
                    _context.Cartons.ToListItems(t => ("Carton - " + t.Item.ItemName),
                        v => v.CartonId.ToString(CultureInfo.InvariantCulture), "Please Select...")
            };


            return View(form);
        }

        [HttpPost, ValidateAntiForgeryToken, Log("Add Carton")]
        public ActionResult AddCarton(CartonInventory formCartonInventory)
        {
            try
            {
                var carton = _context.Inventories.SingleOrDefault(i => i.CartonId == formCartonInventory.CartonId);
                if (carton == null)
                {
                    //TODO: Insert
                    _context.Inventories.Add(new Inventory
                    {
                        CartonId = formCartonInventory.CartonId,
                        NumberOfCartons = formCartonInventory.NumberOfCartons
                    });

                }
                else
                {
                    //TODO: Update
                    var existingCartons = carton.NumberOfCartons;
                    carton.NumberOfCartons = existingCartons + formCartonInventory.NumberOfCartons;
                }

                _context.SaveChanges();

                return RedirectToAction<InventoryController>(c => c.Index())
                .WithSuccess("Carton(s) Added!");
            }
            catch (Exception ex)
            {
                return RedirectToAction<InventoryController>(c => c.Index())
                .WithError("Error Adding Cartons");
            }
        }


        public ActionResult SellCarton()
        {
            var form = new CartonInventory
            {
                CartonList =
                    _context.Cartons.ToListItems(t => ("Carton - " + t.Item.ItemName),
                        v => v.CartonId.ToString(CultureInfo.InvariantCulture), "Please Select...")
            };


            return View(form);
        }

        [HttpPost, ValidateAntiForgeryToken, Log("Sell Carton")]
        public ActionResult SellCarton(CartonInventory formCartonInventory)
        {
            try
            {
                var carton = _context.Inventories.SingleOrDefault(i => i.CartonId == formCartonInventory.CartonId);
                if (carton == null)
                {
                    return RedirectToAction<InventoryController>(c => c.Index())
                        .WithWarning("No Carton Inventory exists!");

                }

                //TODO: Update
                var existingCartons = carton.NumberOfCartons;
                if (formCartonInventory.NumberOfCartons <= existingCartons)
                {
                    carton.NumberOfCartons = existingCartons - formCartonInventory.NumberOfCartons;
                }
                else
                {
                    return RedirectToAction<InventoryController>(c => c.Index())
                        .WithWarning("Carton inventory can't satisfy sell demand!");
                }

                _context.SaveChanges();

                return RedirectToAction<InventoryController>(c => c.Index())
                .WithSuccess("Carton(s) Sold!");
            }
            catch (Exception ex)
            {
                return RedirectToAction<InventoryController>(c => c.Index())
                .WithError("Error Selling Cartons");
            }
        }

        #endregion
    }
}