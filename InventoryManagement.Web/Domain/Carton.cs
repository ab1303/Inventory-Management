using System;
using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.Web.Domain
{
    public class Carton
    {
        [Key]
        public int CartonId { get; set; }

        public int NumberOfPieces { get; set; }

        public int ItemId { get; set; }

        public Item Item { get; set; }

        public DateTime CreatedAt { get; set; }

        public Carton()
        {
            CreatedAt = DateTime.Now;
        }

    }
}