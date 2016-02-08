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
            var form = new InventorySummary
            {
                ItemList =
                      _context.Items.ToListItems(t => (t.ItemCode + " - " + t.ItemName),
                          v => v.ItemId.ToString(CultureInfo.InvariantCulture), "Please Select...")
            };

            return View(form);
        }

        public JsonResult MakeInventory(int itemId)
        {
            string message;
            try
            {
                var item = _context.Items.SingleOrDefault(i => i.ItemId == itemId);
                if (item == null)
                {
                    message = "Unexpected Error!!";
                      return Json(
                          new{ 
                              InvetorySummary = 
                               new
                               {
                                   ItemName = "",
                                   ItemCode="",
                                   NumberOfPieces = -1,
                                   NumberOfCartons = -1,
                                   TotalPieces = -1
                               },
                               Message = message
                        });
                }


                var itemInventory = _context.Inventories.SingleOrDefault(i => i.Carton.ItemId == itemId);
                if (itemInventory == null)
                {
                    //TODO: Divide into cartons and pieces
                    message = "No Inventory defined!!";
                    return Json(
                        new
                        {
                            InvetorySummary =
                             new
                             {
                                 ItemName = item.ItemName,
                                 ItemCode = item.ItemCode,
                                 NumberOfPieces = 0,
                                 NumberOfCartons = 0,
                                 TotalPieces = 0
                             },
                            Message = message
                        });

                }

                var floatPieces = itemInventory.NumberOfPieces;
                var totalCartons = itemInventory.NumberOfCartons;
                var totalPieces = (totalCartons * itemInventory.Carton.NumberOfPieces) + floatPieces;
                message = "Item Inventory made!";
                return Json(
                    new
                    {
                        InvetorySummary =
                         new
                         {
                             ItemName = item.ItemName,
                             ItemCode = item.ItemCode,
                             NumberOfPieces = floatPieces,
                             NumberOfCartons = totalCartons,
                             TotalPieces = totalPieces
                         },
                        Message = message
                    });


            }
            catch (Exception ex)
            {
                message = "Server Error making inventory!";
                return Json(
                         new
                         {
                             InvetorySummary = new {},
                             Message = message
                         });
            }
        }


        # region "Item Inventory"
        public ActionResult AddPiece()
        {
            var form = new ItemInventory
            {
                ItemList =
                      _context.Items.ToListItems(t => (t.ItemCode + " - " + t.ItemName),
                          v => v.ItemId.ToString(CultureInfo.InvariantCulture), "Please Select...")
            };

            return View(form);
        }

        [HttpPost, ValidateAntiForgeryToken, Log("Add Piece")]
        public ActionResult AddPiece(ItemInventory formItemInventory)
        {
            try
            {
                var itemInventory = _context.Inventories.SingleOrDefault(i => i.Carton.ItemId == formItemInventory.ItemId);
                if (itemInventory == null)
                {

                    //TODO: Divide into cartons and pieces
                    var carton = _context.Cartons.SingleOrDefault(i => i.ItemId == formItemInventory.ItemId);
                    if (carton == null)
                    {
                        //TODO: Need to think about a TPH Strategy; throw error for now
                        return RedirectToAction<InventoryController>(c => c.Index())
                            .WithError("No carton defined for respective Item");
                    }

                    int numberOfCartons;
                    int floatPieces;
                    DivideIntoLotSize(formItemInventory.NumberOfPieces, carton.NumberOfPieces, out numberOfCartons,
                        out floatPieces);

                    _context.Inventories.Add(new Inventory
                    {
                        CartonId = carton.CartonId,
                        NumberOfCartons = numberOfCartons,
                        NumberOfPieces = floatPieces
                    });

                }
                else
                {
                    //TODO: Update
                    var carton = _context.Cartons.SingleOrDefault(i => i.ItemId == formItemInventory.ItemId);

                    if (carton == null)
                    {
                        //TODO: This shouldn't happen in business case
                        return RedirectToAction<InventoryController>(c => c.Index())
                            .WithError("Database Error, Contact Administrator");
                    }

                    var existingPieces = itemInventory.NumberOfPieces;
                    var existingCartons = itemInventory.NumberOfCartons;

                    int additionalNumberOfCartons;
                    int floatPieces;
                    DivideIntoLotSize(existingPieces + formItemInventory.NumberOfPieces,
                        carton.NumberOfPieces, out additionalNumberOfCartons,
                        out floatPieces);


                    itemInventory.NumberOfCartons = existingCartons + additionalNumberOfCartons;
                    itemInventory.NumberOfPieces = floatPieces;
                }

                _context.SaveChanges();

                return RedirectToAction<InventoryController>(c => c.Index())
                .WithSuccess("Piece(s) Added!");
            }
            catch (Exception ex)
            {
                return RedirectToAction<InventoryController>(c => c.Index())
                .WithError("Error Adding Piece(s)");
            }
        }

        public ActionResult SellPiece()
        {
            var form = new ItemInventory
            {
                ItemList =
                      _context.Items.ToListItems(t => (t.ItemCode + " - " + t.ItemName),
                          v => v.ItemId.ToString(CultureInfo.InvariantCulture), "Please Select...")
            };

            return View(form);
        }

        [HttpPost, ValidateAntiForgeryToken, Log("Sell Piece")]
        public ActionResult SellPiece(ItemInventory formItemInventory)
        {
            try
            {
                var itemInventory = _context.Inventories.SingleOrDefault(i => i.Carton.ItemId == formItemInventory.ItemId);
                if (itemInventory == null)
                {
                    //TODO: Divide into cartons and pieces
                    return RedirectToAction<InventoryController>(c => c.Index())
                         .WithWarning("No Inventory exists!");

                }
                //TODO: Update
                var carton = _context.Cartons.SingleOrDefault(i => i.ItemId == formItemInventory.ItemId);

                if (carton == null)
                {
                    //TODO: This shouldn't happen in business case
                    return RedirectToAction<InventoryController>(c => c.Index())
                        .WithError("Database Error, Contact Administrator");
                }

                var existingPieces = itemInventory.NumberOfPieces;
                var existingCartons = itemInventory.NumberOfCartons;
                var totalPieces = (existingCartons * carton.NumberOfPieces) + existingPieces;

                if (totalPieces < formItemInventory.NumberOfPieces)
                {
                    return RedirectToAction<InventoryController>(c => c.Index())
                       .WithWarning("Sell Request is more than total inventory");
                }

                int remainingNumberOfCartons;
                int floatPieces;
                DivideIntoLotSize(totalPieces - formItemInventory.NumberOfPieces,
                    carton.NumberOfPieces, out remainingNumberOfCartons,
                    out floatPieces);


                itemInventory.NumberOfCartons = remainingNumberOfCartons;
                itemInventory.NumberOfPieces = floatPieces;

                _context.SaveChanges();

                return RedirectToAction<InventoryController>(c => c.Index())
                .WithSuccess("Piece(s) Sold!");
            }
            catch (Exception ex)
            {
                return RedirectToAction<InventoryController>(c => c.Index())
                .WithError("Error Selling Piece(s)");
            }
        }

        #endregion

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
                var cartonInvetory = _context.Inventories.SingleOrDefault(i => i.CartonId == formCartonInventory.CartonId);
                if (cartonInvetory == null)
                {
                    return RedirectToAction<InventoryController>(c => c.Index())
                        .WithWarning("No Inventory exists!");

                }

                //TODO: Update
                var existingCartons = cartonInvetory.NumberOfCartons;
                if (formCartonInventory.NumberOfCartons <= existingCartons)
                {
                    cartonInvetory.NumberOfCartons = existingCartons - formCartonInventory.NumberOfCartons;
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

        #region "Utility Methods"

        private void DivideIntoLotSize(int totalPieces, int cartonSize, out int numberOfCartons, out int numberOfPieces)
        {
            numberOfCartons = totalPieces / cartonSize;
            numberOfPieces = totalPieces - (numberOfCartons * cartonSize);
        }

        #endregion
    }
}