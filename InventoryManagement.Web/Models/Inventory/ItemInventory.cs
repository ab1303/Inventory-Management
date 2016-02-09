using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using InventoryManagement.Web.Infrastructure.Validation;

namespace InventoryManagement.Web.Models.Inventory
{
    public class ItemInventory
    {
        [Required]
        [MinValue(1)]
        public int NumberOfPieces { get; set; }
        public int ItemId { get; set; }

        public IList<SelectListItem> ItemList { get; set; }
    }
}