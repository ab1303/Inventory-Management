using System.Linq;
using System.Web.Mvc;
using AutoMapper.QueryableExtensions;
using InventoryManagement.Web.Data;
using InventoryManagement.Web.Domain;
using InventoryManagement.Web.Filters;
using InventoryManagement.Web.Infrastructure;
using InventoryManagement.Web.Infrastructure.Alerts;
using InventoryManagement.Web.Models.Item;

namespace InventoryManagement.Web.Controllers
{
    public class ItemController : InventoryManagementController
    {
        private readonly ApplicationDbContext _context;

        public ItemController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: Item
        public ActionResult Index()
        {
            var models = _context.Items
                .Project().To<ItemSummaryViewModel>();
            return View(models.ToArray());
        }


        public ActionResult New()
        {
            var form = new NewItemForm();
            return View(form);
        }

        [HttpPost, ValidateAntiForgeryToken, Log("Created item")]
        public ActionResult New(NewItemForm form)
        {
            if (!ModelState.IsValid)
            {
                return View(form);
            }

            _context.Items.Add(new Item
            {
                ItemName = form.ItemName,
                ItemCode = form.ItemCode,
                Price = form.Price,
            });

            _context.SaveChanges();

            return RedirectToAction<ItemController>(c => c.Index())
                .WithSuccess("Item created!");
        }

    }
}