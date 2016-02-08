using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InventoryManagement.Web.Domain
{
    public class Inventory
    {
        [Key]
        public int InventoryId { get; set; }
        public int NumberOfCartons { get; set; }
        public int NumberOfPieces { get; set; }
        public DateTime CreatedAt { get; set; }
        public int CartonId { get; set; }
        public virtual Carton Carton { get; set; }

        public Inventory()
        {
            CreatedAt = DateTime.Now;
        }
    }



}