using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace InventoryManagement.Web.Models.Carton
{
    public class NewCartonForm
    {
        [Required]
        [Display(Name = "Carton Size")]
        public int NumberOfPieces { get; set; }
        public int ItemId { get; set; }

        public IList<SelectListItem> ItemList { get; set; }

    }
}