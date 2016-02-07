using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InventoryManagement.Web.Domain
{
    public class Item
    {
        [Key]
        public int ItemId { get; set; }

        public string ItemName { get; set; }
        public string ItemCode  { get; set; }
        public decimal Price  { get; set; }
        public DateTime CreatedAt { get; set; }

        public Item()
        {
            CreatedAt = DateTime.Now;
        }

    }
}