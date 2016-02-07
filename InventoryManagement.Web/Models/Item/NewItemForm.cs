using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryManagement.Web.Models.Item
{
    public class NewItemForm
    {
        public string ItemName { get; set; }
        public string ItemCode { get; set; }
        public decimal Price { get; set; }

    }
}