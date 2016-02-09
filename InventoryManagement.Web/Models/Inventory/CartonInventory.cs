using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace InventoryManagement.Web.Models.Inventory
{
    public class CartonInventory
    {
        [Required]
        public int NumberOfCartons { get; set; }
        public int CartonId { get; set; }
        public IList<SelectListItem> CartonList { get; set; }

    }
}