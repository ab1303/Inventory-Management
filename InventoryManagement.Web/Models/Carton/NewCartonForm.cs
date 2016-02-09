using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using InventoryManagement.Web.Infrastructure.Validation;

namespace InventoryManagement.Web.Models.Carton
{
    public class NewCartonForm
    {
        [Required]
        [Display(Name = "Carton Size")]
        [MinValue(1)]
        public int NumberOfPieces { get; set; }
        public int ItemId { get; set; }

        public IList<SelectListItem> ItemList { get; set; }

    }
}