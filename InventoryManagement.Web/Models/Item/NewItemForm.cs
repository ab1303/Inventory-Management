using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InventoryManagement.Web.Models.Item
{
    public class NewItemForm
    {
        [Required]
        public string ItemName { get; set; }
        [Required]
        public string ItemCode { get; set; }
        
        [Required]
        public decimal Price { get; set; }

    }
}