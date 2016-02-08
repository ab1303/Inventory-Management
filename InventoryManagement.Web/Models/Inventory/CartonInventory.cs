using System.Collections.Generic;
using System.Web.Mvc;

namespace InventoryManagement.Web.Models.Inventory
{
    public class CartonInventory
    {
        public int NumberOfCartons { get; set; }
        public int CartonId { get; set; }
        public IList<SelectListItem> CartonList { get; set; }

    }
}